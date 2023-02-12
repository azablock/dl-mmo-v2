using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class HeroLevelText : MonoBehaviour {

        [SerializeField]
        private TMP_Text heroLevelText;
        [SerializeField]
        private XpHolderBehaviour xpHolder;

        private void OnEnable() {
            xpHolder.ClientLevelChanged += ClientOnLevelChanged;   
        }

        private void OnDisable() {
            xpHolder.ClientLevelChanged -= ClientOnLevelChanged;   
        }

        // [Client]
        private void ClientOnLevelChanged(ExperienceLevelChangeEvent evt) => heroLevelText.text = $"{evt.level}";

    }

}