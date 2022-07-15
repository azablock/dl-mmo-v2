using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Bot;
using kcp2k;
using Mirror;
using NSubstitute.Extensions;
using UnityEngine;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkManager : NetworkManager {

        public static Action<NetworkIdentity> clientDarklandPlayerConnected;
        public static Action<NetworkIdentity> clientDarklandPlayerDisconnected;
        public static Action clientDarklandManagerStopped;

        [Space]
        [Header("Darkland Prefabs")]
        public GameObject darklandBotPrefab;

        [SerializeField]
        private DarklandBotManager darklandBotManager;

        public DarklandBotManager DarklandBotManager => darklandBotManager;
        public static DarklandNetworkManager self => singleton as DarklandNetworkManager;
        
        public override void OnValidate() {
            base.OnValidate();
        }

        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Awake() {
            base.Awake();
        }


        /// <summary>
        /// Runs on both Server and Client
        /// Networking is NOT initialized when this fires
        /// </summary>
        public override void Start() {
            var args = Environment.GetCommandLineArgs().ToList();
            
            for (var i = 0; i < args.Count; i++) {
                Debug.Log($"{GetType()}.Start()\tCommand line Argument {i} = {args[i]}");
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

        private IEnumerator StartHeadless(ICollection<string> args) {
            Debug.Log($"{GetType()}.StartHeadless()");
            yield return new WaitForSeconds(2.0f);
        
            var isBot = IsBot(args);
        
            if (isBot) {
                Debug.Log($"{GetType()}: StartHeadless() - Starting Client");
                StartClient();
            } else {
                Debug.Log($"{GetType()}: StartHeadless() - Starting Server");
                StartServer();
            }
        }

        private static bool IsBot(ICollection<string> args) {
            return args.Contains("dl-run-as-bot");
        }

        /// <summary>
        /// Runs on both Server and Client
        /// </summary>
        public override void LateUpdate() {
            base.LateUpdate();
        }

        /// <summary>
        /// Runs on both Server and Client
        /// </summary>
        public override void OnDestroy() {
            base.OnDestroy();
        }

        /// <summary>
        /// called when quitting the application by closing the window / pressing stop in the editor
        /// </summary>
        public override void OnApplicationQuit() {
            base.OnApplicationQuit();
        }

        /// <summary>
        /// This causes the server to switch scenes and sets the networkSceneName.
        /// <para>Clients that connect to this server will automatically switch to this scene. This is called automatically if onlineScene or offlineScene are set,
        /// but it can be called from user code to switch scenes again while the game is in progress. This automatically sets clients to be not-ready. The clients must call NetworkClient.Ready() again to participate in the new scene.
        /// </para>
        /// </summary>
        /// <param name="newSceneName"></param>
        public override void ServerChangeScene(string newSceneName) {
            base.ServerChangeScene(newSceneName);
        }

        /// <summary>
        /// Called from ServerChangeScene immediately before SceneManager.LoadSceneAsync is executed
        /// <para>This allows server to do work / cleanup / prep before the scene changes.</para>
        /// </summary>
        /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
        public override void OnServerChangeScene(string newSceneName) {
        }

        /// <summary>
        /// Called on the server when a scene is completed loaded, when the scene load was initiated by the server with ServerChangeScene().
        /// </summary>
        /// <param name="sceneName">The name of the new scene.</param>
        public override void OnServerSceneChanged(string sceneName) {
        }

        /// <summary>
        /// Called from ClientChangeScene immediately before SceneManager.LoadSceneAsync is executed
        /// <para>This allows client to do work / cleanup / prep before the scene changes.</para>
        /// </summary>
        /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
        /// <param name="sceneOperation">Scene operation that's about to happen</param>
        /// <param name="customHandling">true to indicate that scene loading will be handled through overrides</param>
        public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation,
                                                 bool customHandling) {
        }

        /// <summary>
        /// Called on clients when a scene has completed loaded, when the scene load was initiated by the server.
        /// <para>Scene changes can cause player objects to be destroyed. The default implementation of OnClientSceneChanged in the NetworkManager is to add a player object for the connection if no player object exists.</para>
        /// </summary>
        /// <param name="conn">The network connection that the scene change message arrived on.</param>
        public override void OnClientSceneChanged() {
            base.OnClientSceneChanged();
        }

        /// <summary>
        /// Called on the server when a new client connects.
        /// <para>Unity calls this on the Server when a Client connects to the Server. Use an override to tell the NetworkManager what to do when a client connects to the server.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerConnect(NetworkConnectionToClient conn) {
            base.OnServerConnect(conn);
            // Debug.Log($"{GetType()}.OnServerConnect()\tPlayer [connectionId={conn.connectionId}] connected to the server.");
        }

        /// <summary>
        /// Called on the server when a client is ready.
        /// <para>The default implementation of this function calls NetworkServer.SetClientReady() to continue the network setup process.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerReady(NetworkConnectionToClient conn) {
            base.OnServerReady(conn);
        }

        /// <summary>
        /// Called on the server when a client adds a new player with ClientScene.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerAddPlayer(NetworkConnectionToClient conn) {
            base.OnServerAddPlayer(conn);
        }

        /// <summary>
        /// Called on the server when a client disconnects.
        /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerDisconnect(NetworkConnectionToClient conn) {
            // NetworkServer.SendToAll(new DarklandAuthMessages.DarklandPlayerDisconnectResponseMessage {
            //     disconnectedPlayerNetworkIdentity = conn.identity
            // });

            Debug.Log($"{GetType()}.OnServerDisconnect()\tPlayer [netId={conn.identity.netId}] disconnected from the server.");
            base.OnServerDisconnect(conn);
        }

        
        
        /// <summary>
        /// Called on the client when connected to a server.
        /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
        /// </summary>
        public override void OnClientConnect() {
            base.OnClientConnect();
            var args = Environment.GetCommandLineArgs();
            var isBot = IsBot(args);

            Debug.Log($"{GetType()}.OnClientConnect()\t (isBot={isBot})");

            NetworkClient.connection.Send(new DarklandAuthMessages.DarklandAuthRequestMessage {asBot = isBot});
        }

        /// <summary>
        /// Called on clients when disconnected from a server.
        /// <para>This is called on the client when it disconnects from the server. Override this function to decide what happens when the client disconnects.</para>
        /// </summary>
        /// <param name="conn">Connection to the server.</param>
        public override void OnClientDisconnect() {
            // conn.Send(new DarklandAuthMessages.DarklandPlayerDisconnectRequestMessage());
            base.OnClientDisconnect();
        }

        /// <summary>
        /// Called on clients when a servers tells the client it is no longer ready.
        /// <para>This is commonly used when switching scenes.</para>
        /// </summary>
        /// <param name="conn">Connection to the server.</param>
        public override void OnClientNotReady() {
        }

        // Since there are multiple versions of StartServer, StartClient and StartHost, to reliably customize
        // their functionality, users would need override all the versions. Instead these callbacks are invoked
        // from all versions, so users only need to implement this one case.

        /// <summary>
        /// This is invoked when a host is started.
        /// <para>StartHost has multiple signatures, but they all cause this hook to be called.</para>
        /// </summary>
        public override void OnStartHost() {
        }

        /// <summary>
        /// This is invoked when a server is started - including when a host is started.
        /// <para>StartServer has multiple signatures, but they all cause this hook to be called.</para>
        /// </summary>
        public override void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandAuthMessages.DarklandAuthRequestMessage>(ServerSpawnDarklandPlayer);
            NetworkServer.RegisterHandler<DarklandAuthMessages.DarklandPlayerDisconnectRequestMessage>(ServerOnDisconnectDarklandPlayer);
        }

        private static void ServerOnDisconnectDarklandPlayer(NetworkConnection conn,
                                                             DarklandAuthMessages.DarklandPlayerDisconnectRequestMessage
                                                                 msg) {
            NetworkServer.SendToAll(new DarklandAuthMessages.DarklandPlayerDisconnectResponseMessage {
                    disconnectedPlayerNetworkIdentity = conn.identity
                }
            );
        }

        /// <summary>
        /// This is invoked when the client is started.
        /// </summary>
        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandAuthMessages.DarklandAuthResponseMessage>(
                ClientNotifyDarklandPlayerSpawned
            );
            NetworkClient.RegisterHandler<DarklandAuthMessages.DarklandPlayerDisconnectResponseMessage>(
                ClientNotifyDarklandPlayerDisconnected
            );
        }

        private static void ClientNotifyDarklandPlayerDisconnected(
            DarklandAuthMessages.DarklandPlayerDisconnectResponseMessage msg) {
            Debug.Log("Player disconnected with netId=" + msg.disconnectedPlayerNetworkIdentity != null ? msg.disconnectedPlayerNetworkIdentity.netId : (uint?) null);
            // clientDarklandPlayerDisconnected?.Invoke(msg.disconnectedPlayerNetworkIdentity);
        }

        /// <summary>
        /// This is called when a host is stopped.
        /// </summary>
        public override void OnStopHost() {
        }

        /// <summary>
        /// This is called when a server is stopped - including when a host is stopped.
        /// </summary>
        public override void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandAuthMessages.DarklandAuthRequestMessage>();
            NetworkServer.UnregisterHandler<DarklandAuthMessages.DarklandPlayerDisconnectRequestMessage>();
        }

        /// <summary>
        /// This is called when a client is stopped.
        /// </summary>
        public override void OnStopClient() {
            clientDarklandManagerStopped?.Invoke();
            NetworkClient.UnregisterHandler<DarklandAuthMessages.DarklandAuthResponseMessage>();
            NetworkClient.UnregisterHandler<DarklandAuthMessages.DarklandPlayerDisconnectResponseMessage>();
        }

        [Server]
        private void ServerSpawnDarklandPlayer(NetworkConnectionToClient conn,
                                               DarklandAuthMessages.DarklandAuthRequestMessage msg) {
            var darklandPlayerGameObject = Instantiate(msg.asBot ? darklandBotPrefab : playerPrefab);
            
            NetworkServer.AddPlayerForConnection(conn, darklandPlayerGameObject);

            Debug.Log($"{GetType()}.ServerSpawnDarklandPlayer()\tPlayer [netId={conn.identity.netId}] spawned. (asBot={msg.asBot})");

            NetworkServer.SendToAll(new DarklandAuthMessages.DarklandAuthResponseMessage {
                    spawnedPlayerNetworkIdentity = conn.identity
                }
            );
        }

        [Client]
        private static void ClientNotifyDarklandPlayerSpawned(DarklandAuthMessages.DarklandAuthResponseMessage msg) {
            clientDarklandPlayerConnected?.Invoke(msg.spawnedPlayerNetworkIdentity);
        }
    }

}