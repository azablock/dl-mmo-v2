using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Account;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.Entity;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Persistence;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkManager : NetworkManager {

        [Space]
        [Header("Darkland Prefabs")]
        public GameObject darklandBotPrefab;

        public static DarklandNetworkManager self => singleton as DarklandNetworkManager;
        public DarklandNetworkAuthenticator darklandNetworkAuthenticator => authenticator as DarklandNetworkAuthenticator;

        public struct DisconnectStatus {
            public bool fromServer;
        }

        public static Action<DisconnectStatus> clientDisconnected;
        public static Action<List<string>> clientGetHeroesSuccess;
        public static Action clientNewHeroSuccess;
        public static Action<string> clientNewHeroFailure;
        public static Action clientHeroEnterGameSuccess;

        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Start() {
            var args = Environment.GetCommandLineArgs().ToList();

            for (var i = 0; i < args.Count; i++) {
                // Debug.Log($"{GetType()}.Start()\tCommand line Argument {i} = {args[i]}");
            }

            var remoteServerAddressFlagArgIndex = args.FindIndex(it => it == "dl-server-address");

            if (remoteServerAddressFlagArgIndex > -1 && args.Count > remoteServerAddressFlagArgIndex) {
                networkAddress = args[remoteServerAddressFlagArgIndex + 1];
                Debug.Log($"{GetType()}.Start() NETWORK ADDRESS CHANGED TO {networkAddress}");
            }

            var portFlagArgIndex = args.FindIndex(it => it == "dl-server-port");

            if (portFlagArgIndex > -1 && args.Count > portFlagArgIndex) {
                //todo co z tym zrobic
                // ((KcpTransport) transport).Port = Convert.ToUInt16(args[portFlagArgIndex + 1]);
                // Debug.Log($"NETWORK PORT CHANGED TO {((KcpTransport) transport).Port}");
            }
            
#if !UNITY_EDITOR_64 && !UNITY_SERVER
            networkAddress = "70.34.242.30";
#else
            networkAddress = "localhost";
#endif

            base.Start();

#if !UNITY_EDITOR_64 && UNITY_SERVER
            Debug.Log($"{GetType()}: Start() - Server Starting...");
            StartCoroutine(StartHeadless(args));
            Debug.Log($"{GetType()}: Start() - Server Started!");
#endif
        }

        public override void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandAuthMessages.GetHeroesRequestMessage>(ServerGetHeroes);
            NetworkServer.RegisterHandler<DarklandAuthMessages.NewHeroRequestMessage>(ServerNewHero);
            NetworkServer.RegisterHandler<DarklandAuthMessages.HeroEnterGameRequestMessage>(ServerHeroEnterGame);
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn) {
            DarklandHeroService.ServerSaveDarklandHero(conn.identity.gameObject);
            base.OnServerDisconnect(conn);
        }

        [Server]
        private void ServerGetHeroes(NetworkConnectionToClient conn,
                                             DarklandAuthMessages.GetHeroesRequestMessage msg) {
            var accountName = ((DarklandAuthState) conn.authenticationData).accountName;
            var darklandAccountEntity = DarklandDatabaseManager
                .darklandAccountRepository
                .FindByName(accountName);
            var heroNames = DarklandDatabaseManager
                .darklandHeroRepository
                .FindAllByDarklandAccountId(darklandAccountEntity.id)
                .Select(it => it.name)
                .ToList();

            ((DarklandAuthState) conn.authenticationData).heroNames = heroNames;

            conn.Send(new DarklandAuthMessages.GetDarklandHeroesResponseMessage {heroNames = heroNames});
        }

        [Server]
        private void ServerNewHero(NetworkConnectionToClient conn,
                                              DarklandAuthMessages.NewHeroRequestMessage msg) {
            var heroName = msg.heroName;
            var nameExists = DarklandDatabaseManager.darklandHeroRepository.ExistsByName(heroName);
            var isNameEmpty = string.IsNullOrEmpty(heroName);

            if (nameExists) {
                conn.Send(new DarklandAuthMessages.NewDarklandHeroResponseMessage {success = false, message = "Name taken!"});
            }
            else if (isNameEmpty) {
                conn.Send(new DarklandAuthMessages.NewDarklandHeroResponseMessage {success = false, message = "Name empty!"});
            }
            else {
                var accountName = ((DarklandAuthState) (conn.authenticationData)).accountName;
                var darklandAccountEntity = DarklandDatabaseManager.darklandAccountRepository.FindByName(accountName);

                var darklandHeroEntity = new DarklandHeroEntity {
                    name = heroName,
                    darklandAccountId = darklandAccountEntity.id
                };

                DarklandDatabaseManager.darklandHeroRepository.Create(darklandHeroEntity);

                conn.Send(new DarklandAuthMessages.NewDarklandHeroResponseMessage {
                    success = true,
                    message = "Darkland Hero Created!"
                });
            }
        }

        [Server]
        private void ServerHeroEnterGame(NetworkConnectionToClient conn,
                                             DarklandAuthMessages.HeroEnterGameRequestMessage msg) {
            var isBot = ((DarklandAuthState) conn.authenticationData).isBot; 
            var selectedHeroName = msg.selectedHeroName; //todo save in auth data this value 
            var darklandHeroGameObject = Instantiate(isBot ? darklandBotPrefab : playerPrefab);
            NetworkServer.AddPlayerForConnection(conn, darklandHeroGameObject);

            if (!isBot) {
                DarklandHeroService.ServerLoadDarklandHero(darklandHeroGameObject, selectedHeroName);
            }
            
            conn.Send(new DarklandAuthMessages.DarklandHeroEnterGameResponseMessage());
            
            Debug.Log($"Hero [{conn.identity.netId}] entered at {NetworkTime.time}");
        }

        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandAuthMessages.GetDarklandHeroesResponseMessage>(ClientOnGetDarklandHeroes);
            NetworkClient.RegisterHandler<DarklandAuthMessages.NewDarklandHeroResponseMessage>(ClientOnNewDarklandHero);
            NetworkClient.RegisterHandler<DarklandAuthMessages.DarklandHeroEnterGameResponseMessage>(ClientOnDarklandHeroEnterGame);
        }

        public override void OnClientDisconnect() {
            //todo somehow handle timeout
            clientDisconnected?.Invoke(NetworkClient.connection.isAuthenticated
                                           ? new DisconnectStatus {fromServer = NetworkClient.active}
                                           : new DisconnectStatus {fromServer = true});

            base.OnClientDisconnect();
        }


        [Client]
        private static void ClientOnGetDarklandHeroes(DarklandAuthMessages.GetDarklandHeroesResponseMessage msg) =>
            clientGetHeroesSuccess?.Invoke(msg.heroNames);

        [Client]
        private static void ClientOnNewDarklandHero(DarklandAuthMessages.NewDarklandHeroResponseMessage msg) {
            if (msg.success) {
                clientNewHeroSuccess?.Invoke();
            }
            else {
                clientNewHeroFailure?.Invoke(msg.message);
            }
        }
        
        [Client]
        private void ClientOnDarklandHeroEnterGame(DarklandAuthMessages.DarklandHeroEnterGameResponseMessage msg) {
            Debug.Log($"ClientOnDarklandHeroEnterGame {msg} at + {NetworkTime.time}");
            clientHeroEnterGameSuccess?.Invoke();
        }

        private IEnumerator StartHeadless(ICollection<string> args) {
            Debug.Log($"{GetType()}.StartHeadless()");
            yield return new WaitForSeconds(2.0f);

            var isBot = args.Contains("dl-run-as-bot");

            if (isBot) {
                Debug.Log($"{GetType()}: StartHeadless() - Starting Client");
                StartClient();
            }
            else {
                Debug.Log($"{GetType()}: StartHeadless() - Starting Server");
                StartServer();
            }
        }
    }

}