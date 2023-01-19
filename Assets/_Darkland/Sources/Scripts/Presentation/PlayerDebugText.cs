using _Darkland.Sources.Scripts.Bot;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class PlayerDebugText : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI debugText;

        private UnitNameBehaviour _unitNameBehaviour;

        private void Awake() {
            _unitNameBehaviour = GetComponentInParent<UnitNameBehaviour>();
            _unitNameBehaviour.ClientUnitNameReceived += ClientOnHeroNameReceived;
        }

        private void OnDestroy() {
            _unitNameBehaviour.ClientUnitNameReceived -= ClientOnHeroNameReceived;
        }

        [Client]
        private void ClientOnHeroNameReceived(string heroName) {
            var isLocal = _unitNameBehaviour.isLocalPlayer;
            var isBot = _unitNameBehaviour.GetComponent<DarklandBot>() != null;
            var netId = _unitNameBehaviour.netId;
            var debugTag = isLocal ? "local" : "client";
            var botSymbol = isBot ? "*" : "";

            debugText.text = $"{heroName}{botSymbol}";
            // debugText.text = $"[{netId}]\n({debugTag}{botSymbol})\n{heroName}";
        }
    }

}