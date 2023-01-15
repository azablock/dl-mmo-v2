using _Darkland.Sources.Scripts.Presentation.Account;
using _Darkland.Sources.Scripts.Presentation.Gameplay;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandRootCanvas : MonoBehaviour {
        [SerializeField]
        private AccountRootPanel accountRootPanel;

        [SerializeField]
        private GameplayRootPanel gameplayRootPanel;

        private void OnEnable() {
            DarklandNetworkManager.clientHeroEnterGameSuccess += ClientHeroEnterGameSuccess;
            DarklandNetworkManager.clientOnPlayerDisconnected += OnClientDisconnected;
        }

        private void OnDisable() {
            DarklandNetworkManager.clientHeroEnterGameSuccess -= ClientHeroEnterGameSuccess;
            DarklandNetworkManager.clientOnPlayerDisconnected -= OnClientDisconnected;
        }
        
        [Client]
        private void ClientHeroEnterGameSuccess() {
            accountRootPanel.gameObject.SetActive(false);
            gameplayRootPanel.gameObject.SetActive(true);
        }
        
        private void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            gameplayRootPanel.gameObject.SetActive(false);
            accountRootPanel.gameObject.SetActive(true);
            
            accountRootPanel.OnClientDisconnected(disconnectStatus);
        }
    }

}