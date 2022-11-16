using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation {

    public class ExitGameButton : MonoBehaviour {

        [SerializeField]
        private Button button;

        private void OnEnable() {
            button.onClick.AddListener(Call);
        }

        private void OnDisable() {
            button.onClick.RemoveListener(Call);
        }

        private static void Call() {
            Application.Quit();
        }
    }

}