using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation {

    public class BasicClientInteractionsPanel : MonoBehaviour {

        [SerializeField]
        private Button startClientButton;

        [SerializeField]
        private Button stopClientButton;

        [SerializeField]
        private Button quitGameButton;

        private void Start() {
            stopClientButton.interactable = false;
        }

        private void OnEnable() {
            startClientButton.onClick.AddListener(StartClient);
            stopClientButton.onClick.AddListener(StopClient);
            quitGameButton.onClick.AddListener(QuitGame);
        }

        private void OnDisable() {
            startClientButton.onClick.RemoveListener(StartClient);
            stopClientButton.onClick.RemoveListener(StopClient);
            quitGameButton.onClick.RemoveListener(QuitGame);
        }

        private void StartClient() {
            NetworkManager.singleton.networkAddress = "70.34.242.30"; //todo hehe
            NetworkManager.singleton.StartClient();

            startClientButton.interactable = false;
            stopClientButton.interactable = true;
        }

        private void StopClient() {
            NetworkManager.singleton.StopClient();

            startClientButton.interactable = true;
            stopClientButton.interactable = false;
        }

        private void QuitGame() {
            StopClient();
            Application.Quit();
        }

    }

}