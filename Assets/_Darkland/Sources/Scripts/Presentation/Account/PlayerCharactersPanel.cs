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
        
        public event Action CreateCharacterClicked;
        public event Action<ObjectId> StartClicked;
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

            playerCharactersDropdown.interactable = options.Count > 0;
            playerCharactersDropdown.AddOptions(options);
        }
        
        private void EnterGame() {
            var idx = playerCharactersDropdown.value;
            // var playerCharacterId = ClientAccountStateBehaviour._.accountState.playerCharacters.ToList()[idx].playerCharacterId;
            // StartClicked?.Invoke(playerCharacterId);
        }

        private void CreateCharacter() {
            CreateCharacterClicked?.Invoke();
        }

        private void BackToLogin() {
            BackClicked?.Invoke();
        }
    }

}