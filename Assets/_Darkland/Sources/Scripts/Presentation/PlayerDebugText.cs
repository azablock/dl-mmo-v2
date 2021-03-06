using _Darkland.Sources.Scripts.Bot;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class PlayerDebugText : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI debugText;

        private DarklandPlayer _darklandPlayer;

        private void Awake() {
            _darklandPlayer = GetComponentInParent<DarklandPlayer>();
            _darklandPlayer.ClientStarted += ClientOnClientStarted;
        }

        private void OnDestroy() {
            _darklandPlayer.ClientStarted -= ClientOnClientStarted;
        }

        [Client]
        private void ClientOnClientStarted() {
            var isLocal = _darklandPlayer.isLocalPlayer;
            var isBot = _darklandPlayer.GetComponent<DarklandBot>() != null;
            var netId = _darklandPlayer.netId;
            var debugTag = isLocal ? "local" : isBot ? "bot" : "";

            debugText.text = $"[{netId}]\n({debugTag})";
        }
    }

}