using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class StartServerButton : MonoBehaviour {

        [SerializeField]
        private Button startServerButton;

        private void OnEnable() {
            startServerButton.onClick.AddListener(Call);
        }

        private void OnDisable() {
            startServerButton.onClick.RemoveListener(Call);
        }

        private void Call() {
            DarklandNetworkManager.self.StartServer();
        }
    }

}