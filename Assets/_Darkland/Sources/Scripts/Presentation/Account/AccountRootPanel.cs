using _Darkland.Sources.Models;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class AccountRootPanel : MonoBehaviour {
        [SerializeField]
        private LoginPanel loginPanel;
        [SerializeField]
        private PlayerCharactersPanel playerCharactersPanel;
        [SerializeField]
        private NewCharacterPanel newCharacterPanel;

        private void OnEnable() {
            loginPanel.LoginClicked += LoginPanelOnLoginClicked;
            playerCharactersPanel.StartClicked += PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.CreateCharacterClicked += PlayerCharactersPanelOnCreateCharacterClicked;
            playerCharactersPanel.BackClicked += PlayerCharactersPanelOnBackClicked;
            newCharacterPanel.CreateClicked += NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked += NewCharacterPanelOnBackClicked;
            
            
            DarklandNetworkAuthenticator.ClientAuthRejected += OnClientAuthRejected;
        }

        private static void OnClientAuthRejected() {
            Debug.Log("client rejected at " + NetworkTime.time);
            DarklandNetworkManager.self.StopClient();
        }

        private void OnDisable() {
            loginPanel.LoginClicked -= LoginPanelOnLoginClicked;
            playerCharactersPanel.CreateCharacterClicked -= PlayerCharactersPanelOnCreateCharacterClicked;
            playerCharactersPanel.StartClicked -= PlayerCharactersPanelOnStartClicked;
            playerCharactersPanel.BackClicked -= PlayerCharactersPanelOnBackClicked;
            newCharacterPanel.CreateClicked -= NewCharacterPanelOnCreateClicked;
            newCharacterPanel.BackClicked -= NewCharacterPanelOnBackClicked;
            
            DarklandNetworkAuthenticator.ClientAuthRejected -= OnClientAuthRejected;
        }

        private void LoginPanelOnLoginClicked(string accountName) {
            DarklandNetworkManager.self.darklandNetworkAuthenticator.accountName = accountName;

            //todo
            // if (!NetworkServer.active) {
            //     DarklandNetworkManager.self.StartHost();
            // }
            // else {
            //     DarklandNetworkManager.self.StartClient();
            // }
            DarklandNetworkManager.self.StartClient();

            
            
            
            // ClientAccountStateBehaviour._.accountState.playerCharacters = DarklandDatabaseManager
            //     .darklandPlayerCharacterRepository
            //     .FindAllByDarklandAccountId(ClientAccountStateBehaviour._.accountState.accountId)
            //     .Select(it => new PlayerCharacterState {name = it.name, playerCharacterId = it.id});

            // loginPanel.gameObject.SetActive(false);
            // playerCharactersPanel.gameObject.SetActive(true);
        }

        private void PlayerCharactersPanelOnCreateCharacterClicked() {
            playerCharactersPanel.gameObject.SetActive(false);
            newCharacterPanel.gameObject.SetActive(true);
        }

        private void PlayerCharactersPanelOnBackClicked() {
            playerCharactersPanel.gameObject.SetActive(false);
            loginPanel.gameObject.SetActive(true);
        }

        private void PlayerCharactersPanelOnStartClicked(ObjectId playerCharacterId) {
            // ClientAccountStateBehaviour._.accountState.selectedPlayerCharacterId = playerCharacterId;
            //todo
        }

        private void NewCharacterPanelOnCreateClicked(string characterName) {
            newCharacterPanel.gameObject.SetActive(false);
            playerCharactersPanel.gameObject.SetActive(true);
        }

        private void NewCharacterPanelOnBackClicked() {
            newCharacterPanel.gameObject.SetActive(false);
            playerCharactersPanel.gameObject.SetActive(true);


            // var darklandNetworkAuthenticator = DarklandNetworkManager.authenticator;
            // NetworkClient.Send(new DarklandNetworkAuthenticator.DarklandAuthRequestMessage{request = new DarklandAuthRequest{isBot = false}});
            // darklandNetworkAuthenticator.
        }
    }

}