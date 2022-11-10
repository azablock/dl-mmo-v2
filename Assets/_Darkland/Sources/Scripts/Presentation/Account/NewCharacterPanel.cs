using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class NewCharacterPanel : MonoBehaviour {
        [SerializeField]
        private TMP_InputField characterNameInputField;
        [SerializeField]
        private Button createButton;
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private TMP_Text statusText;

        public event Action<string> CreateClicked;
        public event Action BackClicked;
        public event Action NewPlayerCharacterSuccess;

        private void OnEnable() {
            createButton.onClick.AddListener(CreateCharacter);
            backButton.onClick.AddListener(BackToPlayerCharacters);

            DarklandNetworkManager.clientNewPlayerCharacterSuccess += ClientNewPlayerCharacterSuccess;
            DarklandNetworkManager.clientNewPlayerCharacterFailure += ClientNewPlayerCharacterFailure;

            createButton.interactable = true;
            statusText.text = "Create Character Status";
            characterNameInputField.text = string.Empty;
        }

        private void OnDisable() {
            createButton.onClick.RemoveListener(CreateCharacter);
            backButton.onClick.RemoveListener(BackToPlayerCharacters);

            DarklandNetworkManager.clientNewPlayerCharacterSuccess -= ClientNewPlayerCharacterSuccess;
            DarklandNetworkManager.clientNewPlayerCharacterFailure -= ClientNewPlayerCharacterFailure;
        }

        private void CreateCharacter() {
            createButton.interactable = false;
            
            var characterName = characterNameInputField.text;
            CreateClicked?.Invoke(characterName);
        }

        private void ClientNewPlayerCharacterSuccess() {
            createButton.interactable = true;
            NewPlayerCharacterSuccess?.Invoke();
        }

        private void ClientNewPlayerCharacterFailure(string message) {
            createButton.interactable = true;
            statusText.text = message;
        }

        private void BackToPlayerCharacters() {
            BackClicked?.Invoke();
        }
    }

}