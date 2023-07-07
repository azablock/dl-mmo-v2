using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Account {

    public class StopClientButton : MonoBehaviour {

        [SerializeField]
        private Button logoutButton;

        private void OnEnable() {
            logoutButton.onClick.AddListener(Call);
        }

        private void OnDisable() {
            logoutButton.onClick.RemoveListener(Call);
        }

        private void Call() {
            DarklandNetworkManager.self.StopClient();
        }

    }

}