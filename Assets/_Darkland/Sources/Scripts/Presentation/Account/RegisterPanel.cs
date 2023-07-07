using System;
using Castle.Core.Internal;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class RegisterPanel : MonoBehaviour {

        [SerializeField]
        private TMP_InputField accountNameInputField;
        [SerializeField]
        private Button registerButton;
        [SerializeField]
        private Button backButton;
        [SerializeField]
        private TMP_Text registerStatusText;

        private void OnEnable() {
            registerButton.onClick.AddListener(OnRegisterClicked);
            backButton.onClick.AddListener(OnBackClicked);
            DarklandNetworkAuthenticator.clientAuthSuccess += OnClientAuthSuccess;

            accountNameInputField.text = string.Empty;
            registerStatusText.text = "Register Status";
        }

        private void OnDisable() {
            registerButton.onClick.RemoveListener(OnRegisterClicked);
            backButton.onClick.RemoveListener(OnBackClicked);
            DarklandNetworkAuthenticator.clientAuthSuccess -= OnClientAuthSuccess;
        }

        public event Action<string> RegisterClicked;
        public event Action RegisterSuccess;
        public event Action BackClicked;

        private void OnRegisterClicked() {
            if (accountNameInputField.text.IsNullOrEmpty()) {
                registerStatusText.text = "Name cannot be empty";
                return;
            }

            RegisterClicked?.Invoke(accountNameInputField.text);
        }

        private void OnBackClicked() {
            BackClicked?.Invoke();
        }

        private void OnClientAuthSuccess() {
            Debug.Log("client auth success (register) at " + NetworkTime.time);
            RegisterSuccess?.Invoke();
        }

    }

}