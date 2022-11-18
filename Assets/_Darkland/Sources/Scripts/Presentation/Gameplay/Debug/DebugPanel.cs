using _Darkland.Sources.Scripts.DiscretePosition;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Debug {

    public class DebugPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text localPlayerPosText;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<DiscretePositionBehaviour>().ClientChanged += ClientOnPosChanged;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<DiscretePositionBehaviour>().ClientChanged -= ClientOnPosChanged;
        }

        [Client]
        private void ClientOnPosChanged(Vector3Int pos) {
            localPlayerPosText.text = $"Pos {pos}";
        }
    }

}