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
        private Button registerButton;
        [SerializeField]
        private TMP_Text loginStatusText;

        public event Action<string> LoginClicked;
        public event Action LoginSuccess;
        public event Action RegisterClicked;

        private void OnEnable() {
            loginButton.onClick.AddListener(SubmitLogin);
            registerButton.onClick.AddListener(OnRegisterClicked);
            DarklandNetworkAuthenticator.clientAuthSuccess += OnClientAuthSuccess;
            DarklandNetworkAuthenticator.clientAuthFailure += OnClientAuthFailure;

            accountNameInputField.text = string.Empty;
            loginStatusText.text = "Login Status";
        }

        private void OnDisable() {
            loginButton.onClick.RemoveListener(SubmitLogin);
            registerButton.onClick.RemoveListener(OnRegisterClicked);
            DarklandNetworkAuthenticator.clientAuthSuccess -= OnClientAuthSuccess;
            DarklandNetworkAuthenticator.clientAuthFailure -= OnClientAuthFailure;
        }
        
        private void SubmitLogin() {
            loginStatusText.text = "Loading...";
            LoginClicked?.Invoke(accountNameInputField.text);
        }

        private void OnRegisterClicked() {
            RegisterClicked?.Invoke();
        }

        private void OnClientAuthSuccess() {
            loginStatusText.text = "Login successful!";

            Debug.Log("client authenticated at " + NetworkTime.time);

            LoginSuccess?.Invoke();
        }

        private void OnClientAuthFailure() {
            loginStatusText.text = "Account does not exist!";

            Debug.Log("client rejected at " + NetworkTime.time);
            DarklandNetworkManager.self.StopClient();
        }
        
        public void OnClientDisconnected(DarklandNetworkManager.DisconnectStatus disconnectStatus) {
            if (disconnectStatus.fromServer) {
                loginStatusText.text = "Disconnected from server!";
                Debug.Log("server stopped at " + NetworkTime.time);
            }
            else {
                loginStatusText.text = "Login Status - client triggered";
                Debug.Log("client disconnected from server at " + NetworkTime.time);
            }

            DarklandNetworkManager.self.StopClient();
        }
    }

}