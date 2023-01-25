using System.Collections.Generic;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

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

        private void OnEnable() {
            loginPanel.LoginClicked += LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked += LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess += GetHeroes;

            registerPanel.RegisterClicked += RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked += BackToLogin;
            registerPanel.RegisterSuccess += GetHeroes;

            heroesPanel.StartClicked += HeroesPanelOnStartClicked;
            heroesPanel.NewHeroClicked += HeroesPanelOnNewHeroClicked;
            heroesPanel.BackClicked += BackToLogin;

            newHeroPanel.CreateClicked += NewHeroPanelOnCreateClicked;
            newHeroPanel.BackClicked += GetHeroes;
            newHeroPanel.NewHeroSuccess += GetHeroes;

            DarklandNetworkAuthenticator.clientGetHeroesSuccess += ClientGetHeroesSuccess;
        }

        private void OnDisable() {
            loginPanel.LoginClicked -= LoginPanelOnLoginClicked;
            loginPanel.RegisterClicked -= LoginPanelOnRegisterClicked;
            loginPanel.LoginSuccess -= GetHeroes;

            registerPanel.RegisterClicked -= RegisterPanelOnRegisterClicked;
            registerPanel.BackClicked -= BackToLogin;
            registerPanel.RegisterSuccess -= GetHeroes;

            heroesPanel.StartClicked -= HeroesPanelOnStartClicked;
            heroesPanel.NewHeroClicked -= HeroesPanelOnNewHeroClicked;
            heroesPanel.BackClicked -= BackToLogin;

            newHeroPanel.CreateClicked -= NewHeroPanelOnCreateClicked;
            newHeroPanel.BackClicked -= GetHeroes;
            newHeroPanel.NewHeroSuccess -= GetHeroes;

            DarklandNetworkAuthenticator.clientGetHeroesSuccess -= ClientGetHeroesSuccess;
        }

        private static void LoginPanelOnLoginClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = false;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            StartConnection();
        }

        private void LoginPanelOnRegisterClicked() => ShowChildPanel(registerPanel);

        private static void RegisterPanelOnRegisterClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientIsRegister = true;
            DarklandNetworkManager.self.darklandNetworkAuthenticator.clientAccountName = accountName;

            StartConnection();
        }

        private void HeroesPanelOnNewHeroClicked() => ShowChildPanel(newHeroPanel);

        private static void HeroesPanelOnStartClicked(string heroName) =>
            NetworkClient.Send(new DarklandAuthMessages.HeroEnterGameRequestMessage { selectedHeroName = heroName });

        private static void NewHeroPanelOnCreateClicked(DarklandHeroDto formData) =>
            NetworkClient.Send(new DarklandAuthMessages.NewHeroRequestMessage {
                heroName = formData.heroName,
                heroVocation = formData.heroVocation
            });

        private void ClientGetHeroesSuccess(List<DarklandHeroDto> heroes) {
            ShowChildPanel(heroesPanel);
            heroesPanel.Init(heroes);
        }

        public void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            ShowChildPanel(loginPanel);
            loginPanel.OnClientDisconnected(disconnectStatus);
        }

        private void BackToLogin() {
            DarklandNetworkManager.self.StopClient();
            ShowChildPanel(loginPanel);
        }

        private static void GetHeroes() => NetworkClient.Send(new DarklandAuthMessages.GetHeroesRequestMessage());

        private void ShowChildPanel(Component panelComponent) {
            for (var i = 0; i < transform.childCount; i++) {
                var childPanelGameObject = transform.GetChild(i).gameObject;
                childPanelGameObject.SetActive(Equals(panelComponent.gameObject, childPanelGameObject));
            }
        }

        private static void StartConnection() {
            // NetworkManager.singleton.StartClient();

            
#if UNITY_EDITOR_64 && !UNITY_SERVER
            if (!NetworkServer.active) {
                DarklandNetworkManager.self.StartHost();
            } else {
                DarklandNetworkManager.self.StartClient();
            }
#else
                NetworkManager.singleton.StartClient();
#endif
        }
    }

}