using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class DarklandUnitInfoPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text unitNameText;
        [SerializeField]
        private Slider unitHealthSlider;
        [SerializeField]
        private TMP_Text unitHealthText;

        [Client]
        public void ClientSetUnitName(string unitName) => unitNameText.text = unitName;

        [Client]
        public void ClientSetMaxHealth(float val) {
            unitHealthSlider.maxValue = val;
            ClientSetHealthText();
        }

        [Client]
        public void ClientSetHealth(float val) {
            unitHealthSlider.value = val;
            ClientSetHealthText();
        }

        [Client]
        private void ClientSetHealthText() => unitHealthText.text = $"{unitHealthSlider.value} / {unitHealthSlider.maxValue}";

    }

}