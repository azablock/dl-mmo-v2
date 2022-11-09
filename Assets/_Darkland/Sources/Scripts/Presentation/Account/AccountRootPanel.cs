using System.Collections.Generic;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class AccountRootPanel : MonoBehaviour {
        [SerializeField]
        private LoginPanel loginPanel;
        [SerializeField]
        private RegisterPanel registerPanel;
        [SerializeField]
        private PlayerCharactersPanel playerCharactersPanel;
        [SerializeField]
        private NewCharacterPanel newCharacterPanel;

        private void OnEnable() {
            loginPanel.LoginClicked += LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked += LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess += GetPlayerCharacters;

            registerPanel.RegisterClicked += RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked += BackToLogin;
            registerPanel.RegisterSuccess += GetPlayerCharacters;

            playerCharactersPanel.StartClicked += PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.CreateCharacterClicked += PlayerCharactersPanelOnCreateCharacterClicked;
            playerCharactersPanel.BackClicked += BackToLogin;

            newCharacterPanel.CreateClicked += NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked += GetPlayerCharacters;

            DarklandNetworkManager.clientGetPlayerCharactersSuccess += ClientGetPlayerCharactersSuccess;
            DarklandNetworkManager.clientDisconnected += OnClientDisconnected;
        }

        private void OnDisable() {
            loginPanel.LoginClicked -= LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked -= LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess -= GetPlayerCharacters;

            registerPanel.RegisterClicked -= RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked -= BackToLogin;
            registerPanel.RegisterSuccess -= GetPlayerCharacters;

            playerCharactersPanel.CreateCharacterClicked -= PlayerCharactersPanelOnCreateCharacterClicked;
            playerCharactersPanel.StartClicked -= PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.BackClicked -= BackToLogin;

            newCharacterPanel.CreateClicked -= NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked -= GetPlayerCharacters;

            DarklandNetworkManager.clientGetPlayerCharactersSuccess -= ClientGetPlayerCharactersSuccess;
            DarklandNetworkManager.clientDisconnected -= OnClientDisconnected;
        }

        private void LoginPanelOnLoginClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = false;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            StartHostOrClient();
        }

        private void LoginPanelOnRegisterClicked() => ShowChildPanel(registerPanel);

        private void RegisterPanelOnRegisterClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = true;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            StartHostOrClient();
        }
        
        private void PlayerCharactersPanelOnCreateCharacterClicked() => ShowChildPanel(newCharacterPanel);

        private void PlayerCharactersPanelOnStartClicked(ObjectId playerCharacterId) {
            // ClientAccountStateBehaviour._.accountState.selectedPlayerCharacterId = playerCharacterId;
            //todo enter game
        }

        private void NewCharacterPanelOnCreateClicked(string characterName) {
            //request for player characters list and return to playerCharactersPanel
        }

        private void ClientGetPlayerCharactersSuccess(List<string> playerCharacterNames) {
            ShowChildPanel(playerCharactersPanel);
            playerCharactersPanel.Init(playerCharacterNames);
        }
        
        private void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            // throw new System.NotImplementedException();
            ShowChildPanel(loginPanel);
            loginPanel.OnClientDisconnected(disconnectStatus);
        }

        private void BackToLogin() {
            DarklandNetworkManager.self.StopClient();
            ShowChildPanel(loginPanel);
        }

        private static void GetPlayerCharacters() => NetworkClient.Send(new DarklandAuthMessages.GetPlayerCharactersRequestMessage());

        private static void StartHostOrClient() {
// #if UNITY_EDITOR_64 && !UNITY_SERVER
// #endif

            DarklandNetworkManager.self.StartClient();
            // if (!NetworkServer.active) {
            //     DarklandNetworkManager.self.StartHost();
            // }
            // else {
            //     DarklandNetworkManager.self.StartClient();
            // }
        }

        private void ShowChildPanel(Component panelComponent) {
            for (var i = 0; i < transform.childCount; i++) {
                var childPanelGameObject = transform.GetChild(i).gameObject;
                childPanelGameObject.SetActive(Equals(panelComponent.gameObject, childPanelGameObject));
            }
        }

    }

}