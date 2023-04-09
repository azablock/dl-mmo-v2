using System;
using System.Linq;
using _Darkland.Sources.Models.GameReport;
using _Darkland.Sources.NetworkMessages;
using Castle.Core.Internal;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.GameReport {

    public class GameReportPanel : MonoBehaviour {

        [SerializeField]
        private TMP_InputField titleInputField;
        [SerializeField]
        private TMP_InputField contentInputField;
        [SerializeField]
        private TMP_Dropdown gameReportTypeDropdown;
        [SerializeField]
        private Button sendButton;
        [SerializeField]
        private Button closeButton;

        public static event Action Enabled;
        public static event Action Disabled;

        private void OnEnable() {
            sendButton.onClick.AddListener(ClientSendGameReport);
            closeButton.onClick.AddListener(ClientHide);
            gameReportTypeDropdown.AddOptions(Enum.GetNames(typeof(GameReportType)).ToList());
            gameReportTypeDropdown.value = 0;
            
            Enabled?.Invoke();
        }

        private void OnDisable() {
            sendButton.onClick.RemoveListener(ClientSendGameReport);
            closeButton.onClick.RemoveListener(ClientHide);
            gameReportTypeDropdown.ClearOptions();
            
            titleInputField.text = string.Empty;
            contentInputField.text = string.Empty;
            
            Disabled?.Invoke();
        }

        [Client]
        private void ClientSendGameReport() {
            var title = titleInputField.text.Trim();
            var content = contentInputField.text.Trim();

            if (title.IsNullOrEmpty() || content.IsNullOrEmpty()) return;
            
            NetworkClient.Send(new ChatMessages.GameReportRequestMessage {
                title = title,
                content = content,
                gameReportType = Enum.Parse<GameReportType>(gameReportTypeDropdown.options[gameReportTypeDropdown.value].text)
            });

            ClientHide();
        }

        [Client]
        private void ClientHide() => gameObject.SetActive(false);
    }

}