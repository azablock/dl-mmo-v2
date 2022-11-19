using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class HeroesPanel : MonoBehaviour {
        [SerializeField]
        private TMP_Dropdown heroesDropdown;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button createHeroButton;
        [SerializeField]
        private Button backButton;
        
        public event Action NewHeroClicked;
        public event Action<string> StartClicked;
        public event Action BackClicked;

        private void OnEnable() {
            heroesDropdown.interactable = false;
            startButton.onClick.AddListener(EnterGame);
            createHeroButton.onClick.AddListener(CreateHero);
            backButton.onClick.AddListener(BackToLogin);
        }

        private void OnDisable() {
            heroesDropdown.ClearOptions();
            startButton.onClick.RemoveListener(EnterGame);
            createHeroButton.onClick.RemoveListener(CreateHero);
            backButton.onClick.RemoveListener(BackToLogin);
        }

        public void Init(IEnumerable<string> heroNames) {
            var options = heroNames
            .Select(it => new TMP_Dropdown.OptionData(it))
            .ToList();

            var hasOptions = options.Count > 0;
            heroesDropdown.interactable = hasOptions;
            startButton.interactable = hasOptions;
            heroesDropdown.AddOptions(options);

            if (hasOptions) {
                heroesDropdown.value = 0;
            }
        }

        private void EnterGame() => StartClicked?.Invoke(heroesDropdown.captionText.text);

        private void CreateHero() {
            NewHeroClicked?.Invoke();
        }

        private void BackToLogin() {
            BackClicked?.Invoke();
        }
    }

}