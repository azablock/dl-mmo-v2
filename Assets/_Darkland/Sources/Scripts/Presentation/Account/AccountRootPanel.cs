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
        private HeroesPanel heroesPanel;
        [SerializeField]
        private NewHeroPanel newHeroPanel;
        [SerializeField]
        private Image backgroundImage;

        private void OnEnable() {
            loginPanel.LoginClicked += LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked += LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess += GetHero;

            registerPanel.RegisterClicked += RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked += BackToLogin;
            registerPanel.RegisterSuccess += GetHero;

            heroesPanel.StartClicked += HeroesPanelOnStartClicked;
            heroesPanel.NewHeroClicked += HeroesPanelOnNewHeroClicked;
            heroesPanel.BackClicked += BackToLogin;

            newHeroPanel.CreateClicked += NewHeroPanelOnCreateClicked;
            newHeroPanel.BackClicked += GetHero;
            newHeroPanel.NewHeroSuccess += GetHero;

            DarklandNetworkManager.clientGetHeroesSuccess += ClientGetHeroesSuccess;
            DarklandNetworkManager.clientHeroEnterGameSuccess += ClientHeroEnterGameSuccess;
            DarklandNetworkManager.clientDisconnected += OnClientDisconnected;
        }

        private void OnDisable() {
            loginPanel.LoginClicked -= LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked -= LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess -= GetHero;

            registerPanel.RegisterClicked -= RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked -= BackToLogin;
            registerPanel.RegisterSuccess -= GetHero;

            heroesPanel.StartClicked -= HeroesPanelOnStartClicked;
            heroesPanel.NewHeroClicked -= HeroesPanelOnNewHeroClicked;
            heroesPanel.BackClicked -= BackToLogin;

            newHeroPanel.CreateClicked -= NewHeroPanelOnCreateClicked;
            newHeroPanel.BackClicked -= GetHero;
            newHeroPanel.NewHeroSuccess -= GetHero;

            DarklandNetworkManager.clientGetHeroesSuccess -= ClientGetHeroesSuccess;
            DarklandNetworkManager.clientHeroEnterGameSuccess -= ClientHeroEnterGameSuccess;
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

        private void HeroesPanelOnNewHeroClicked() => ShowChildPanel(newHeroPanel);

        private static void HeroesPanelOnStartClicked(string heroName) =>
            NetworkClient.Send(new DarklandAuthMessages.HeroEnterGameRequestMessage {selectedHeroName = heroName});

        private static void NewHeroPanelOnCreateClicked(string heroName) =>
            NetworkClient.Send(new DarklandAuthMessages.NewHeroRequestMessage {heroName = heroName});

        private void ClientGetHeroesSuccess(List<string> heroNames) {
            ShowChildPanel(heroesPanel);
            heroesPanel.Init(heroNames);
        }

        private void ClientHeroEnterGameSuccess() => Hide();

        private void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            backgroundImage.enabled = true;
            ShowChildPanel(loginPanel);
            loginPanel.OnClientDisconnected(disconnectStatus);
        }

        private void BackToLogin() {
            DarklandNetworkManager.self.StopClient();
            ShowChildPanel(loginPanel);
        }

        private static void GetHero() => NetworkClient.Send(new DarklandAuthMessages.GetHeroesRequestMessage());

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