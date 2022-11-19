using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Debug {

    public class DebugPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text localPlayerPosText;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnPosChanged;
            ClientOnPosChanged(DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos);
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnPosChanged;
        }

        [Client]
        private void ClientOnPosChanged(Vector3Int pos) {
            localPlayerPosText.text = $"Pos {pos}";
        }
    }

}