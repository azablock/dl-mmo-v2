using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class PlayerCharactersPanel : MonoBehaviour {
        [SerializeField]
        private TMP_Dropdown playerCharactersDropdown;
        [SerializeField]
        private Button startButton;
        [SerializeField]
        private Button createCharacterButton;
        [SerializeField]
        private Button backButton;
        
        public event Action NewCharacterClicked;
        public event Action<string> StartClicked;
        public event Action BackClicked;

        private void OnEnable() {
            playerCharactersDropdown.interactable = false;
            startButton.onClick.AddListener(EnterGame);
            createCharacterButton.onClick.AddListener(CreateCharacter);
            backButton.onClick.AddListener(BackToLogin);
        }

        private void OnDisable() {
            playerCharactersDropdown.ClearOptions();
            startButton.onClick.RemoveListener(EnterGame);
            createCharacterButton.onClick.RemoveListener(CreateCharacter);
        }

        public void Init(IEnumerable<string> playerCharacterNames) {
            var options = playerCharacterNames
            .Select(it => new TMP_Dropdown.OptionData(it))
            .ToList();

            var hasOptions = options.Count > 0;
            playerCharactersDropdown.interactable = hasOptions;
            startButton.interactable = hasOptions;
            playerCharactersDropdown.AddOptions(options);

            if (hasOptions) {
                playerCharactersDropdown.value = 0;
            }
        }

        private void EnterGame() => StartClicked?.Invoke(playerCharactersDropdown.captionText.text);

        private void CreateCharacter() {
            NewCharacterClicked?.Invoke();
        }

        private void BackToLogin() {
            BackClicked?.Invoke();
        }
    }

}