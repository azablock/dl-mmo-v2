using _Darkland.Sources.Scripts.Bot;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class PlayerDebugText : MonoBehaviour {
        
        [SerializeField]
        private TextMeshProUGUI debugText;

        private DarklandHero _darklandHero;

        private void Awake() {
            _darklandHero = GetComponentInParent<DarklandHero>();
            _darklandHero.ClientHeroNameSet += ClientOnHeroNameSet;
        }

        private void OnDestroy() {
            _darklandHero.ClientHeroNameSet -= ClientOnHeroNameSet;
        }

        [Client]
        private void ClientOnHeroNameSet(string heroName) {
            var isLocal = _darklandHero.isLocalPlayer;
            var isBot = _darklandHero.GetComponent<DarklandBot>() != null;
            var netId = _darklandHero.netId;
            var debugTag = isLocal ? "local" : "client";
            var botSymbol = isBot ? "*" : "";

            debugText.text = $"[{netId}]\n({debugTag}{botSymbol})\n{heroName}";
        }
    }

}