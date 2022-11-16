using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class StopServerButton : MonoBehaviour {

        [SerializeField]
        private Button stopServerButton;

        private void OnEnable() {
            stopServerButton.onClick.AddListener(Call);
        }

        private void OnDisable() {
            stopServerButton.onClick.RemoveListener(Call);
        }

        private void Call() {
            DarklandNetworkManager.self.StopServer();
        }
    }

}