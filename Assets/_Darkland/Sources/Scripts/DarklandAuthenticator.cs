using System;
using _Darkland.Sources.Models;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandAuthenticator : NetworkAuthenticator {

        #region Messages

        public struct DarklandAuthRequestMessage : NetworkMessage {
            public DarklandAuthData authData;
        }

        public struct DarklandAuthResponseMessage : NetworkMessage {
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
        public override void OnServerAuthenticate(NetworkConnection conn) {
        }

        /// <summary>
        /// Called on server when the client's AuthRequestMessage arrives
        /// </summary>
        /// <param name="conn">Connection to client.</param>
        /// <param name="msg">The message payload</param>
        public void OnAuthRequestMessage(NetworkConnection conn, DarklandAuthRequestMessage msg) {
            DarklandAuthResponseMessage darklandAuthResponseMessage = new DarklandAuthResponseMessage();

            conn.Send(darklandAuthResponseMessage);

            // Accept the successful authentication
            ServerAccept(conn);
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

            DarklandAuthRequestMessage darklandAuthRequestMessage = new DarklandAuthRequestMessage {
                authData = new DarklandAuthData {isBot = args.Length > 1 && args[1] == "c"}
            };

            NetworkClient.Send(darklandAuthRequestMessage);
        }

        /// <summary>
        /// Called on client when the server's AuthResponseMessage arrives
        /// </summary>
        /// <param name="msg">The message payload</param>
        public void OnAuthResponseMessage(DarklandAuthResponseMessage msg) {
            // Authentication has been accepted
            ClientAccept();
        }

        #endregion

    }

}