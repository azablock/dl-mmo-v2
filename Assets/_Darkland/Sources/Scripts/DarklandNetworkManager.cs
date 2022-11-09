using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Account;
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

        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandAuthMessages.GetPlayerCharactersResponseMessage>(ClientOnGetPlayerCharacters);
        }
        
        public override void OnClientDisconnect() {
            //todo somehow handle timeout
            if (NetworkClient.connection.isAuthenticated) {
                clientDisconnected?.Invoke(new DisconnectStatus {fromServer = NetworkClient.active});
            }

            base.OnClientDisconnect();
        }

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

        [Client]
        private void ClientOnGetPlayerCharacters(DarklandAuthMessages.GetPlayerCharactersResponseMessage msg) {
            clientGetPlayerCharactersSuccess?.Invoke(msg.playerCharacterNames);
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