using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
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

        public void Init(List<DarklandHeroDto> heroes) {
            var options = heroes
            .Select(it => new TMP_Dropdown.OptionData($"{it.heroName} ({it.heroVocationType.ToString()})"))
            .ToList();

            var hasOptions = options.Count > 0;
            heroesDropdown.interactable = hasOptions;
            startButton.interactable = hasOptions;
            heroesDropdown.AddOptions(options);

            if (hasOptions) {
                heroesDropdown.value = 0;
            }
        }

        //todo hack - trzeba value miec dropdowna jako tylko imie gracza - ale view pokazuje name + vocation
        private void EnterGame() => StartClicked?.Invoke(heroesDropdown.captionText.text.Split(" (")[0]);

        private void CreateHero() {
            NewHeroClicked?.Invoke();
        }

        private void BackToLogin() {
            BackClicked?.Invoke();
        }
    }

}