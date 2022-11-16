using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class NewHeroPanel : MonoBehaviour {
        [SerializeField]
        private TMP_InputField heroNameInputField;
        [SerializeField]
        private Button createButton;
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private TMP_Text statusText;

        public event Action<string> CreateClicked;
        public event Action BackClicked;
        public event Action NewHeroSuccess;

        private void OnEnable() {
            createButton.onClick.AddListener(CreateHero);
            backButton.onClick.AddListener(BackToHeroes);

            DarklandNetworkManager.clientNewHeroSuccess += ClientNewHeroSuccess;
            DarklandNetworkManager.clientNewHeroFailure += ClientNewHeroFailure;

            createButton.interactable = true;
            statusText.text = "Create Darkland Hero Status";
            heroNameInputField.text = string.Empty;
        }

        private void OnDisable() {
            createButton.onClick.RemoveListener(CreateHero);
            backButton.onClick.RemoveListener(BackToHeroes);

            DarklandNetworkManager.clientNewHeroSuccess -= ClientNewHeroSuccess;
            DarklandNetworkManager.clientNewHeroFailure -= ClientNewHeroFailure;
        }

        private void CreateHero() {
            createButton.interactable = false;
            
            var characterName = heroNameInputField.text;
            CreateClicked?.Invoke(characterName);
        }

        private void ClientNewHeroSuccess() {
            createButton.interactable = true;
            NewHeroSuccess?.Invoke();
        }

        private void ClientNewHeroFailure(string message) {
            createButton.interactable = true;
            statusText.text = message;
        }

        private void BackToHeroes() {
            BackClicked?.Invoke();
        }
    }

}