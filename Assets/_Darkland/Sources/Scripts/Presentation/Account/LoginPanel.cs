using System;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class LoginPanel : MonoBehaviour {
        [SerializeField]
        private TMP_InputField accountNameInputField;
        [SerializeField]
        private Button loginButton;
        [SerializeField]
        private TMP_Text loginStatusText;

        public event Action<string> LoginClicked;

        private void OnEnable() {
            loginButton.onClick.AddListener(SubmitLogin);
            DarklandNetworkAuthenticator.ClientAuthSuccess += OnClientAuthSuccess;
            DarklandNetworkAuthenticator.ClientAuthFailure += OnClientAuthFailure;
            
            loginStatusText.text = "Status";
        }

        private void OnDisable() {
            loginButton.onClick.RemoveListener(SubmitLogin);
            DarklandNetworkAuthenticator.ClientAuthSuccess -= OnClientAuthSuccess;
            DarklandNetworkAuthenticator.ClientAuthFailure -= OnClientAuthFailure;
        }

        private void SubmitLogin() {
            loginStatusText.text = "Loading...";
            LoginClicked?.Invoke(accountNameInputField.text);
        }

        private void OnClientAuthSuccess() {
            loginStatusText.text = "Login successful!";
            
            Debug.Log("client authenticated at " + NetworkTime.time);
        }

        private void OnClientAuthFailure() {
            loginStatusText.text = "Account does not exist!";

            Debug.Log("client rejected at " + NetworkTime.time);
            DarklandNetworkManager.self.StopClient();
        }

    }

}