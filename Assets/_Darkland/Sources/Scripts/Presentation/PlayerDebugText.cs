using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Bot;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class PlayerDebugText : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI heroNameAndLevelText;
        private string _unitLevel;
        private string _unitName;

        private UnitNameBehaviour _unitNameBehaviour;
        private IXpHolder _xpHolder;

        private void Awake() {
            _unitNameBehaviour = GetComponentInParent<UnitNameBehaviour>();
            _xpHolder = GetComponentInParent<IXpHolder>();

            _unitNameBehaviour.ClientUnitNameReceived += ClientOnHeroNameReceived;
            _xpHolder.ClientLevelChanged += ClientOnLevelChanged;
        }

        private void OnDestroy() {
            _unitNameBehaviour.ClientUnitNameReceived -= ClientOnHeroNameReceived;
            _xpHolder.ClientLevelChanged -= ClientOnLevelChanged;
        }

        [Client]
        private void ClientOnHeroNameReceived(string heroName) {
            var isLocal = _unitNameBehaviour.isLocalPlayer;
            var isBot = _unitNameBehaviour.GetComponent<DarklandBot>() != null;
            var netId = _unitNameBehaviour.netId;
            var debugTag = isLocal ? "local" : "client";
            var botSymbol = isBot ? "*" : "";

            _unitName = $"{heroName}{botSymbol}";
            ClientRefresh();
            // debugText.text = $"[{netId}]\n({debugTag}{botSymbol})\n{heroName}";
        }

        [Client]
        private void ClientOnLevelChanged(ExperienceLevelChangeEvent evt) {
            _unitLevel = $"[{evt.level}]";
            ClientRefresh();
        }

        [Client]
        private void ClientRefresh() {
            heroNameAndLevelText.text = $"{_unitLevel} {_unitName}";
        }

    }

}