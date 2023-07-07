using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Account;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.DarklandHero;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkManager : NetworkManager {

        [Space]
        [Header("Darkland Prefabs")]
        public GameObject darklandPlayerPrefab;
        public GameObject darklandBotPrefab;

        public static DarklandNetworkManager self => singleton as DarklandNetworkManager;
        public DarklandNetworkAuthenticator darklandNetworkAuthenticator => authenticator as DarklandNetworkAuthenticator;

        public struct DisconnectStatus {
            public bool fromServer;
        }

        private List<INetworkMessagesProxy> _networkMessagesProxies;

        public static Action ServerStarted;
        public static Action<NetworkIdentity> serverOnPlayerDisconnected;
        public static Action<DisconnectStatus> clientOnPlayerDisconnected;
        public static Action clientHeroEnterGameSuccess;

        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Start() {
            //todo move somewhere
            Physics.IgnoreLayerCollision(
                LayerMask.NameToLayer($"Player"),
                LayerMask.NameToLayer($"Player"),
                true);
            
            Physics.IgnoreLayerCollision(
                LayerMask.NameToLayer($"Mob"),
                LayerMask.NameToLayer($"Mob"),
                true);
            
            _networkMessagesProxies = GetComponentsInChildren<INetworkMessagesProxy>().ToList();
            
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
            NetworkServer.RegisterHandler<DarklandAuthMessages.HeroEnterGameRequestMessage>(ServerHeroEnterGame);
            _networkMessagesProxies.ForEach(it => it.OnStartServer());
            
            ServerStarted?.Invoke();

            FindObjectOfType<DayNightCycleHolderBehaviour>().ServerSet(0);

        }

        public override void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandAuthMessages.HeroEnterGameRequestMessage>();
            _networkMessagesProxies.ForEach(it => it.OnStopServer());
        }

        public override void OnServerDisconnect(NetworkConnectionToClient conn) {
            var netIdentity = conn.identity;

            if (netIdentity != null) {
                var heroName = netIdentity.GetComponent<UnitNameBehaviour>().unitName;
                var message = RichTextFormatter.FormatServerLog($"{heroName} has left the game.");

                NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage {message = message});
                DarklandHeroService.ServerSaveDarklandHero(netIdentity.gameObject);
                
                serverOnPlayerDisconnected?.Invoke(netIdentity);
                
                Debug.Log(message);
            }
            
            base.OnServerDisconnect(conn);
        }

        // public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            // base.OnServerAddPlayer(conn);
        // }
        
        [Server]
        private void ServerHeroEnterGame(NetworkConnectionToClient conn,
                                         DarklandAuthMessages.HeroEnterGameRequestMessage msg) {
            var isBot = ((DarklandAuthState) conn.authenticationData).isBot; 
            var selectedHeroName = msg.selectedHeroName; //todo save in auth data this value 
            var darklandHeroGameObject = Instantiate(isBot ? darklandBotPrefab : darklandPlayerPrefab);

            if (!isBot) {
                DarklandHeroService.ServerLoadDarklandHero(darklandHeroGameObject, selectedHeroName);
            }
            
            NetworkServer.AddPlayerForConnection(conn, darklandHeroGameObject);
            conn.Send(new DarklandAuthMessages.DarklandHeroEnterGameResponseMessage());
            
            var heroName = conn.identity.GetComponent<UnitNameBehaviour>().unitName;
            var message = RichTextFormatter.FormatServerLog($"{heroName} has joined the game.");

            NetworkServer.SendToReadyObservers(
                conn.identity,
                new ChatMessages.ServerLogResponseMessage { message = message },
                false
            );
            
            Debug.Log(message);
        }

        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandAuthMessages.DarklandHeroEnterGameResponseMessage>(ClientOnDarklandHeroEnterGame);

            _networkMessagesProxies.ForEach(it => it.OnStartClient());
        }

        public override void OnStopClient() {
            NetworkClient.UnregisterHandler<DarklandAuthMessages.DarklandHeroEnterGameResponseMessage>();
            
            _networkMessagesProxies.ForEach(it => it.OnStopClient());
        }

        public override void OnClientDisconnect() {
            clientOnPlayerDisconnected?.Invoke(NetworkClient.connection.isAuthenticated
                                           ? new DisconnectStatus {fromServer = NetworkClient.active}
                                           : new DisconnectStatus {fromServer = true});
            base.OnClientDisconnect();
        }
        
        [Client]
        private void ClientOnDarklandHeroEnterGame(DarklandAuthMessages.DarklandHeroEnterGameResponseMessage msg) =>
            clientHeroEnterGameSuccess?.Invoke();

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