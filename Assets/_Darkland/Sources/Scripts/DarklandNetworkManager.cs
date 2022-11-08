using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models;
using _Darkland.Sources.NetworkMessages;
using kcp2k;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkManager : NetworkManager {

        public static Action<NetworkIdentity> clientDarklandPlayerConnected;
        public static Action clientDarklandManagerStopped;

        [Space]
        [Header("Darkland Prefabs")]
        public GameObject darklandBotPrefab;

        public static DarklandNetworkManager self => singleton as DarklandNetworkManager;
        public DarklandNetworkAuthenticator darklandNetworkAuthenticator => authenticator as DarklandNetworkAuthenticator;

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

        // public override void OnClientDisconnect() {
        //     base.OnClientDisconnect();
        //     var authData = (DarklandAuthResponse)NetworkClient.connection.authenticationData;
        //
        //     if (!authData.success) {
        //         DarklandNetworkAuthenticator.ClientAuthRejected?.Invoke();
        //     }
        // }


        [Server]
        private void ServerSpawnDarklandPlayer(NetworkConnectionToClient conn,
                                               DarklandAuthMessages.DarklandAuthRequestMessage msg) {
            var darklandPlayerGameObject = Instantiate(msg.asBot ? darklandBotPrefab : playerPrefab);
            
            
            NetworkServer.AddPlayerForConnection(conn, darklandPlayerGameObject);
            
            //todo init/bootstrap process
            // darklandPlayerGameObject.GetComponent<StatsHolder>().Stat(StatId.MovementSpeed).Set(StatValue.OfBasic(1.0f));

            Debug.Log($"{GetType()}.ServerSpawnDarklandPlayer()\tPlayer [netId={conn.identity.netId}] spawned. (asBot={msg.asBot})");

            NetworkServer.SendToAll(new DarklandAuthMessages.DarklandAuthResponseMessage {
                    spawnedPlayerNetworkIdentity = conn.identity
                }
            );
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
    }

}