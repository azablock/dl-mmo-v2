using System;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Scripts.Presentation.Account.NewHero;
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
        [Space]
        [Space]
        [SerializeField]
        private HeroVocationPanel heroVocationPanel;

        public event Action<DarklandHeroDto> CreateClicked;
        public event Action BackClicked;
        public event Action NewHeroSuccess;

        private DarklandHeroDto _formData;

        private void OnEnable() {
            createButton.onClick.AddListener(CreateHero);
            backButton.onClick.AddListener(BackToHeroes);

            DarklandNetworkAuthenticator.clientNewHeroSuccess += ClientNewHeroSuccess;
            DarklandNetworkAuthenticator.clientNewHeroFailure += ClientNewHeroFailure;

            heroVocationPanel.VocationSelected += OnVocationSelected;

            _formData = new DarklandHeroDto();

            createButton.interactable = true;
            statusText.text = "Create Darkland Hero Status";

            heroNameInputField.text = string.Empty;
            heroNameInputField.onValueChanged.AddListener(OnHeroNameValueChanged);
        }

        private void OnDisable() {
            createButton.onClick.RemoveListener(CreateHero);
            backButton.onClick.RemoveListener(BackToHeroes);

            DarklandNetworkAuthenticator.clientNewHeroSuccess -= ClientNewHeroSuccess;
            DarklandNetworkAuthenticator.clientNewHeroFailure -= ClientNewHeroFailure;
            
            heroVocationPanel.VocationSelected -= OnVocationSelected;
            heroNameInputField.onValueChanged.RemoveListener(OnHeroNameValueChanged);
        }

        private void CreateHero() {
            createButton.interactable = false;
            CreateClicked?.Invoke(_formData);
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

        private void OnVocationSelected(HeroVocationType vocationType) => _formData.heroVocationType = vocationType;

        private void OnHeroNameValueChanged(string val) {
            var cleanVal = Regex.Replace(val.Trim(), @"[^\w\s]", string.Empty);
            _formData.heroName = cleanVal;
            
            if (val == cleanVal) return;
            heroNameInputField.text = cleanVal;
        }

    }

}