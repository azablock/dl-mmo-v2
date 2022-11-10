using System.Collections.Generic;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField]
        private Image backgroundImage;

        private void OnEnable() {
            loginPanel.LoginClicked += LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked += LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess += GetPlayerCharacters;

            registerPanel.RegisterClicked += RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked += BackToLogin;
            registerPanel.RegisterSuccess += GetPlayerCharacters;

            playerCharactersPanel.StartClicked += PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.NewCharacterClicked += PlayerCharactersPanelOnNewCharacterClicked;
            playerCharactersPanel.BackClicked += BackToLogin;

            newCharacterPanel.CreateClicked += NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked += GetPlayerCharacters;
            newCharacterPanel.NewPlayerCharacterSuccess += GetPlayerCharacters;

            DarklandNetworkManager.clientGetPlayerCharactersSuccess += ClientGetPlayerCharactersSuccess;
            DarklandNetworkManager.clientPlayerEnterGameSuccess += ClientPlayerEnterGameSuccess;
            DarklandNetworkManager.clientDisconnected += OnClientDisconnected;
        }

        private void OnDisable() {
            loginPanel.LoginClicked -= LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked -= LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess -= GetPlayerCharacters;

            registerPanel.RegisterClicked -= RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked -= BackToLogin;
            registerPanel.RegisterSuccess -= GetPlayerCharacters;

            playerCharactersPanel.StartClicked -= PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.NewCharacterClicked -= PlayerCharactersPanelOnNewCharacterClicked;
            playerCharactersPanel.BackClicked -= BackToLogin;

            newCharacterPanel.CreateClicked -= NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked -= GetPlayerCharacters;
            newCharacterPanel.NewPlayerCharacterSuccess -= GetPlayerCharacters;

            DarklandNetworkManager.clientGetPlayerCharactersSuccess -= ClientGetPlayerCharactersSuccess;
            DarklandNetworkManager.clientPlayerEnterGameSuccess -= ClientPlayerEnterGameSuccess;
            DarklandNetworkManager.clientDisconnected -= OnClientDisconnected;
        }

        private static void LoginPanelOnLoginClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = false;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            NetworkManager.singleton.StartClient();
        }

        private void LoginPanelOnRegisterClicked() => ShowChildPanel(registerPanel);

        private static void RegisterPanelOnRegisterClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = true;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            NetworkManager.singleton.StartClient();
        }

        private void PlayerCharactersPanelOnNewCharacterClicked() => ShowChildPanel(newCharacterPanel);

        private static void PlayerCharactersPanelOnStartClicked(string playerCharacterName) =>
            NetworkClient.Send(new DarklandAuthMessages.PlayerEnterGameRequestMessage {selectedPlayerCharacterName = playerCharacterName});

        private static void NewCharacterPanelOnCreateClicked(string playerCharacterName) =>
            NetworkClient.Send(new DarklandAuthMessages.NewPlayerCharacterRequestMessage {playerCharacterName = playerCharacterName});

        private void ClientGetPlayerCharactersSuccess(List<string> playerCharacterNames) {
            ShowChildPanel(playerCharactersPanel);
            playerCharactersPanel.Init(playerCharacterNames);
        }

        private void ClientPlayerEnterGameSuccess() => Hide();

        private void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            backgroundImage.enabled = true;
            ShowChildPanel(loginPanel);
            loginPanel.OnClientDisconnected(disconnectStatus);
        }

        private void BackToLogin() {
            DarklandNetworkManager.self.StopClient();
            ShowChildPanel(loginPanel);
        }

        private static void GetPlayerCharacters() => NetworkClient.Send(new DarklandAuthMessages.GetPlayerCharactersRequestMessage());

        private void ShowChildPanel(Component panelComponent) {
            for (var i = 0; i < transform.childCount; i++) {
                var childPanelGameObject = transform.GetChild(i).gameObject;
                childPanelGameObject.SetActive(Equals(panelComponent.gameObject, childPanelGameObject));
            }
        }

        private void Hide() {
            for (var i = 0; i < transform.childCount; i++) {
                var childPanelGameObject = transform.GetChild(i).gameObject;
                childPanelGameObject.SetActive(false);
            }

            backgroundImage.enabled = false;
        }

    }

}