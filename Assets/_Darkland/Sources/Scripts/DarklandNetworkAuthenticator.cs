using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Account;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.DarklandHero;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Persistence;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandNetworkAuthenticator : NetworkAuthenticator {

        [HideInInspector]
        public bool clientIsRegister;
        [HideInInspector]
        public string clientAccountName;
        public static Action clientAuthSuccess;
        public static Action clientAuthFailure;
        public static Action clientTimeout;
        
        public static Action<List<DarklandHeroDto>> clientGetHeroesSuccess;
        public static Action clientNewHeroSuccess;
        public static Action<string> clientNewHeroFailure;

        #region Messages

        public struct DarklandAuthRequestMessage : NetworkMessage {
            public bool isBot;
            public bool isRegister;
            public string accountName;
        }

        public struct DarklandAuthResponseMessage : NetworkMessage {
            public bool success;
            public string message;
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
            NetworkServer.RegisterHandler<DarklandAuthMessages.GetHeroesRequestMessage>(ServerGetHeroes);
            NetworkServer.RegisterHandler<DarklandAuthMessages.NewHeroRequestMessage>(ServerNewHero);
        }

        public override void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandAuthRequestMessage>();
            NetworkServer.UnregisterHandler<DarklandAuthMessages.GetHeroesRequestMessage>();
            NetworkServer.UnregisterHandler<DarklandAuthMessages.NewHeroRequestMessage>();
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
            if (msg.isRegister && !msg.isBot) {
                ServerResolveRegister(conn, msg);
            }
            else {
                ServerResolveLogin(conn, msg);
            }
        }

        private void ServerResolveRegister(NetworkConnectionToClient conn, DarklandAuthRequestMessage msg) {
            var accountName = msg.accountName;
            var accountNameEmpty = string.IsNullOrEmpty(accountName);
            var accountExists = DarklandDatabaseManager.darklandAccountRepository.ExistsByName(accountName);
            var isValid = !accountNameEmpty && !accountExists;

            if (isValid) {
                // Accept the successful authentication
                DarklandDatabaseManager.darklandAccountRepository.Create(accountName);
                ServerAccept(conn);
            }
            else {
                StartCoroutine(nameof(ServerRejectAfterFrame), conn);
            }

            conn.authenticationData = new DarklandAuthState {accountName = accountName, isBot = false};
            conn.Send(new DarklandAuthResponseMessage {success = isValid});
        }

        private void ServerResolveLogin(NetworkConnectionToClient conn, DarklandAuthRequestMessage msg) {
            var accountName = msg.accountName;
            var accountExists = DarklandDatabaseManager.darklandAccountRepository.ExistsByName(accountName);
            var isBot = msg.isBot;
            var isValid = accountExists || isBot;

            if (isValid) {
                // Accept the successful authentication
                ServerAccept(conn);
                // conn.Send();
            }
            else {
                StartCoroutine(nameof(ServerRejectAfterFrame), conn);
            }

            conn.authenticationData = new DarklandAuthState {accountName = accountName, isBot = isBot};
            conn.Send(new DarklandAuthResponseMessage {success = isValid});
        }

        private IEnumerator ServerRejectAfterFrame(NetworkConnectionToClient conn) {
            yield return null;
            yield return null;

            conn.isAuthenticated = false;
            ServerReject(conn);
        }

          [Server]
        private void ServerGetHeroes(NetworkConnectionToClient conn,
                                             DarklandAuthMessages.GetHeroesRequestMessage msg) {
            var accountName = ((DarklandAuthState) conn.authenticationData).accountName;
            var darklandAccountEntity = DarklandDatabaseManager
                .darklandAccountRepository
                .FindByName(accountName);
            var heroes = DarklandDatabaseManager
                .darklandHeroRepository
                .FindAllByDarklandAccountId(darklandAccountEntity.id)
                .Select(it => new DarklandHeroDto{heroVocationType = Enum.Parse<HeroVocationType>(it.vocation), heroName = it.name})
                .ToList();

            ((DarklandAuthState) conn.authenticationData).heroNames = heroes.Select(it => it.heroName).ToList();

            conn.Send(new DarklandAuthMessages.GetDarklandHeroesResponseMessage {heroes = heroes});
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

                DarklandHeroService.ServerCreateNewHero(darklandAccountEntity.id, heroName, msg.heroVocationType);

                conn.Send(new DarklandAuthMessages.NewDarklandHeroResponseMessage {
                    success = true,
                    message = "Darkland Hero Created!"
                });
            }
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
            NetworkClient.RegisterHandler<DarklandAuthMessages.GetDarklandHeroesResponseMessage>(ClientOnGetDarklandHeroes);
            NetworkClient.RegisterHandler<DarklandAuthMessages.NewDarklandHeroResponseMessage>(ClientOnNewDarklandHero);
        }

        public override void OnStopClient() {
            NetworkClient.UnregisterHandler<DarklandAuthResponseMessage>();
            NetworkClient.UnregisterHandler<DarklandAuthMessages.GetDarklandHeroesResponseMessage>();
            NetworkClient.UnregisterHandler<DarklandAuthMessages.NewDarklandHeroResponseMessage>();
        }

        /// <summary>
        /// Called on client from OnClientAuthenticateInternal when a client needs to authenticate
        /// </summary>
        public override void OnClientAuthenticate() {
            base.OnClientAuthenticate();
            // if (clientTimeoutSeconds > 0)
                // StartCoroutine(BeginAuthentication(NetworkClient.connection));
            
            var args = Environment.GetCommandLineArgs();
            var isBot = args.Length > 1 && args[1] == "c";

            NetworkClient.Send(new DarklandAuthRequestMessage {
                accountName = clientAccountName,
                isRegister = clientIsRegister,
                isBot = isBot
            });
            

        }

        /// <summary>
        /// Called on client when the server's AuthResponseMessage arrives
        /// </summary>
        /// <param name="msg">The message payload</param>
        public void OnAuthResponseMessage(DarklandAuthResponseMessage msg) {
            if (msg.success) {
                // Authentication has been accepted
                ClientAccept(); //todo not yet
                clientAuthSuccess?.Invoke();
            }
            else {
                clientAuthFailure?.Invoke();

                if (NetworkClient.connection != null) {
                    ClientReject();
                }
            }
        }
        
        [Client]
        private static void ClientOnGetDarklandHeroes(DarklandAuthMessages.GetDarklandHeroesResponseMessage msg) =>
            clientGetHeroesSuccess?.Invoke(msg.heroes);

        [Client]
        private static void ClientOnNewDarklandHero(DarklandAuthMessages.NewDarklandHeroResponseMessage msg) {
            if (msg.success) {
                clientNewHeroSuccess?.Invoke();
            }
            else {
                clientNewHeroFailure?.Invoke(msg.message);
            }
        }



        #endregion
    }

}