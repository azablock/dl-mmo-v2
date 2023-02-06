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
        [SerializeField]
        private Slider unitManaSlider;
        [SerializeField]
        private TMP_Text unitManaText;

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
        public void ClientSetMaxMana(float val) {
            unitManaSlider.maxValue = val;
            ClientSetManaText();
        }

        [Client]
        public void ClientSetMana(float val) {
            unitManaSlider.value = val;
            ClientSetManaText();
        }

        [Client]
        private void ClientSetHealthText() => unitHealthText.text = $"{unitHealthSlider.value} / {unitHealthSlider.maxValue}";

        private void ClientSetManaText() => unitManaText.text = $"{unitManaSlider.value} / {unitManaSlider.maxValue}";

    }

}