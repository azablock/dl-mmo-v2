using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Account;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Persistence;
using kcp2k;
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
        public static Action<List<string>> clientGetPlayerCharactersSuccess;
        public static Action clientNewPlayerCharacterSuccess;
        public static Action<string> clientNewPlayerCharacterFailure;
        public static Action clientPlayerEnterGameSuccess;

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
                ((KcpTransport) transport).Port = Convert.ToUInt16(args[portFlagArgIndex + 1]);
                Debug.Log($"NETWORK PORT CHANGED TO {((KcpTransport) transport).Port}");
            }

            base.Start();

#if !UNITY_EDITOR_64 && UNITY_SERVER
            Debug.Log($"{GetType()}: Start() - Server Starting...");
            StartCoroutine(StartHeadless(args));
            Debug.Log($"{GetType()}: Start() - Server Started!");
#endif
        }

        public override void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandAuthMessages.GetPlayerCharactersRequestMessage>(ServerGetPlayerCharacters);
            NetworkServer.RegisterHandler<DarklandAuthMessages.NewPlayerCharacterRequestMessage>(ServerNewPlayerCharacter);
            NetworkServer.RegisterHandler<DarklandAuthMessages.PlayerEnterGameRequestMessage>(ServerOnPlayerEnterGame);
        }

        [Server]
        private void ServerGetPlayerCharacters(NetworkConnectionToClient conn,
                                               DarklandAuthMessages.GetPlayerCharactersRequestMessage msg) {
            var accountName = ((DarklandAuthState) conn.authenticationData).accountName;
            var darklandAccountEntity = DarklandDatabaseManager
                .darklandAccountRepository
                .FindByName(accountName);
            var playerCharacterNames = DarklandDatabaseManager
                .darklandPlayerCharacterRepository
                .FindAllByDarklandAccountId(darklandAccountEntity.id)
                .Select(it => it.name)
                .ToList();

            ((DarklandAuthState) conn.authenticationData).playerCharacterNames = playerCharacterNames;

            conn.Send(new DarklandAuthMessages.GetPlayerCharactersResponseMessage {playerCharacterNames = playerCharacterNames});
        }

        [Server]
        private void ServerNewPlayerCharacter(NetworkConnectionToClient conn,
                                              DarklandAuthMessages.NewPlayerCharacterRequestMessage msg) {
            var playerCharacterName = msg.playerCharacterName;
            var nameExists = DarklandDatabaseManager.darklandPlayerCharacterRepository.ExistsByName(playerCharacterName);
            var isNameEmpty = string.IsNullOrEmpty(playerCharacterName);

            if (nameExists) {
                conn.Send(new DarklandAuthMessages.NewPlayerCharacterResponseMessage {success = false, message = "Name taken!"});
            }
            else if (isNameEmpty) {
                conn.Send(new DarklandAuthMessages.NewPlayerCharacterResponseMessage {success = false, message = "Name empty!"});
            }
            else {
                var accountName = ((DarklandAuthState) (conn.authenticationData)).accountName;
                var darklandAccountEntity = DarklandDatabaseManager.darklandAccountRepository.FindByName(accountName);

                var darklandPlayerCharacterEntity = new DarklandPlayerCharacterEntity {
                    name = playerCharacterName,
                    darklandAccountId = darklandAccountEntity.id
                };

                DarklandDatabaseManager.darklandPlayerCharacterRepository.Create(darklandPlayerCharacterEntity);

                conn.Send(new DarklandAuthMessages.NewPlayerCharacterResponseMessage {
                    success = true,
                    message = "Player Character Created!"
                });
            }
        }

        [Server]
        private void ServerOnPlayerEnterGame(NetworkConnectionToClient conn,
                                             DarklandAuthMessages.PlayerEnterGameRequestMessage msg) {
            var isBot = ((DarklandAuthState) conn.authenticationData).isBot; 
            var darklandPlayerGameObject = Instantiate(isBot ? darklandBotPrefab : playerPrefab);
        
            NetworkServer.AddPlayerForConnection(conn, darklandPlayerGameObject);
            conn.Send(new DarklandAuthMessages.PlayerEnterGameResponseMessage());
        
            //todo init/bootstrap process
            // darklandPlayerGameObject.GetComponent<StatsHolder>().Stat(StatId.MovementSpeed).Set(StatValue.OfBasic(1.0f));
        
            // NetworkServer.SendToAll(new DarklandAuthMessages.DarklandAuthResponseMessage {
                // spawnedPlayerNetworkIdentity = conn.identity
            // });
        }

        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandAuthMessages.GetPlayerCharactersResponseMessage>(ClientOnGetPlayerCharacters);
            NetworkClient.RegisterHandler<DarklandAuthMessages.NewPlayerCharacterResponseMessage>(ClientOnNewPlayerCharacter);
            NetworkClient.RegisterHandler<DarklandAuthMessages.PlayerEnterGameResponseMessage>(ClientOnPlayerEnterGame);
        }

        public override void OnClientConnect() {
            base.OnClientConnect();

            Debug.Log($"OnClientConnect (ready state) at {NetworkTime.time}");
        }

        public override void OnClientDisconnect() {
            //todo somehow handle timeout
            clientDisconnected?.Invoke(NetworkClient.connection.isAuthenticated
                                           ? new DisconnectStatus {fromServer = NetworkClient.active}
                                           : new DisconnectStatus {fromServer = true});

            base.OnClientDisconnect();
        }


        [Client]
        private static void ClientOnGetPlayerCharacters(DarklandAuthMessages.GetPlayerCharactersResponseMessage msg) =>
            clientGetPlayerCharactersSuccess?.Invoke(msg.playerCharacterNames);

        [Client]
        private static void ClientOnNewPlayerCharacter(DarklandAuthMessages.NewPlayerCharacterResponseMessage msg) {
            if (msg.success) {
                clientNewPlayerCharacterSuccess?.Invoke();
            }
            else {
                clientNewPlayerCharacterFailure?.Invoke(msg.message);
            }
        }
        
        [Client]
        private void ClientOnPlayerEnterGame(DarklandAuthMessages.PlayerEnterGameResponseMessage msg) {
            Debug.Log($"ClientOnPlayerEnterGame {msg} at + {NetworkTime.time}");
            clientPlayerEnterGameSuccess?.Invoke();
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