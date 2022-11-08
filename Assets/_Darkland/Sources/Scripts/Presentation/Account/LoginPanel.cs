using System;
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
            loginStatusText.text = "Status";
        }

        private void OnDisable() {
            loginButton.onClick.RemoveListener(SubmitLogin);
        }

        private void SubmitLogin() {
            LoginClicked?.Invoke(accountNameInputField.text);
            // if (darklandAccountEntity == null) {
            //     loginStatusText.text = "Account does not exist!";
            // }
            // else {
            //     loginStatusText.text = $"Found {darklandAccountEntity.name} account";
            // }
        }
    }

}