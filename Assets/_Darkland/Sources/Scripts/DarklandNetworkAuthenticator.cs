using System;
using System.Collections;
using _Darkland.Sources.Models;
using _Darkland.Sources.Scripts.Persistence;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkAuthenticator : NetworkAuthenticator {

        public string accountName;
        public static Action ClientAuthSuccess;
        public static Action ClientAuthFailure;
        
        #region Messages

        public struct DarklandAuthRequestMessage : NetworkMessage {
            public DarklandAuthRequest request;
        }

        public struct DarklandAuthResponseMessage : NetworkMessage {
            public DarklandAuthResponse response;
        }

        #endregion

        #region Server

        /// <summary>
        /// Called on server from StartServer to initialize the Authenticator
        /// <para>Server message handlers should be registered in this method.</para>
        /// </summary>
        public override void OnStartServer() {
            // register a handler for the authentication request we expect from client
            NetworkServer.RegisterHandler<DarklandAuthRequestMessage>(OnAuthRequestMessage, false);
        }

        /// <summary>
        /// Called on server from OnServerAuthenticateInternal when a client needs to authenticate
        /// </summary>
        /// <param name="conn">Connection to client.</param>
        public override void OnServerAuthenticate(NetworkConnectionToClient conn) {
            // conn.authenticationData
        }

        /// <summary>
        /// Called on server when the client's AuthRequestMessage arrives
        /// </summary>
        /// <param name="conn">Connection to client.</param>
        /// <param name="msg">The message payload</param>
        public void OnAuthRequestMessage(NetworkConnectionToClient conn, DarklandAuthRequestMessage msg) {
            var darklandAccountEntity = DarklandDatabaseManager.darklandAccountRepository.FindByName(msg.request.accountName);
            var accountExists = darklandAccountEntity != null;
            var response = new DarklandAuthResponse {success = accountExists};
            var darklandAuthResponseMessage = new DarklandAuthResponseMessage {response = response};

            conn.authenticationData = response;
            conn.Send(darklandAuthResponseMessage);
            
            if (accountExists) {
                // Accept the successful authentication
                ServerAccept(conn);    
            }
            else {
                StartCoroutine(nameof(ServerRejectAfterFrame), conn);
            }
        }

        private IEnumerator ServerRejectAfterFrame(NetworkConnectionToClient conn) {
            yield return null;
            yield return null;

            conn.isAuthenticated = false;
            ServerReject(conn);
        }

        #endregion

        #region Client

        /// <summary>
        /// Called on client from StartClient to initialize the Authenticator
        /// <para>Client message handlers should be registered in this method.</para>
        /// </summary>
        public override void OnStartClient() {
            // register a handler for the authentication response we expect from server
            NetworkClient.RegisterHandler<DarklandAuthResponseMessage>(OnAuthResponseMessage, false);
        }

        /// <summary>
        /// Called on client from OnClientAuthenticateInternal when a client needs to authenticate
        /// </summary>
        public override void OnClientAuthenticate() {
            var args = Environment.GetCommandLineArgs();
            var isBot = args.Length > 1 && args[1] == "c";

            var darklandAuthRequestMessage = new DarklandAuthRequestMessage {
                request = new DarklandAuthRequest {isBot = isBot, accountName = accountName},
            };

            NetworkClient.Send(darklandAuthRequestMessage);
        }

        /// <summary>
        /// Called on client when the server's AuthResponseMessage arrives
        /// </summary>
        /// <param name="msg">The message payload</param>
        public void OnAuthResponseMessage(DarklandAuthResponseMessage msg) {
            if (msg.response.success) {
                // Authentication has been accepted
                ClientAccept();
                ClientAuthSuccess?.Invoke();
            }
            else {
                ClientAuthFailure?.Invoke();

                if (NetworkClient.connection != null) {
                    ClientReject();
                }
            }
        }

        #endregion

    }

}