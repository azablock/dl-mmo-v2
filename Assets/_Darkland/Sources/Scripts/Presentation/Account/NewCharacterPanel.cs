using System;
using _Darkland.Sources.Scripts.Persistence;
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
        
        private void OnEnable() {
            createButton.onClick.AddListener(CreateCharacter);
            backButton.onClick.AddListener(BackToPlayerCharacters);
        }

        private void OnDisable() {
            createButton.onClick.RemoveListener(BackToPlayerCharacters);
            backButton.onClick.RemoveListener(BackToPlayerCharacters);
        }

        private void CreateCharacter() {
            var characterName = characterNameInputField.text;
            var alreadyExists = DarklandDatabaseManager.darklandPlayerCharacterRepository.ExistsByName(characterName);

            if (alreadyExists) {
                statusText.text = "Name taken!";
            }
            else {
                statusText.text = "Name valid!";
                
                //todo db add
                CreateClicked?.Invoke(characterName);
            }
        }

        private void BackToPlayerCharacters() {
            BackClicked?.Invoke();
        }
    }

}