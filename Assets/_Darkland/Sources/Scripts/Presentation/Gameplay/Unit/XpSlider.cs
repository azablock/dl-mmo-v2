using _Darkland.Sources.Models.Unit;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class XpSlider : MonoBehaviour {

        [SerializeField]
        private Slider xpSlider;
        [SerializeField]
        private TMP_Text xpText;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientLevelChanged += ClientOnLevelChanged;
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientXpChanged += ClientOnXpChanged;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientLevelChanged -= ClientOnLevelChanged;
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientXpChanged -= ClientOnXpChanged;
        }

        [Client]
        private void ClientOnLevelChanged(ExperienceLevelChangeEvent evt) {
            xpSlider.minValue = evt.currentLevelXpCap;
            xpSlider.maxValue = evt.nextLevelXpCap;
            xpSlider.value = evt.currentXp;
            ClientSetSliderText();
        }

        [Client]
        private void ClientOnXpChanged(int xp) {
            xpSlider.value = xp;
            ClientSetSliderText();
        }

        [Client]
        private void ClientSetSliderText() => xpText.text =
            $"{(xpSlider.value - xpSlider.minValue) * 100 / (xpSlider.maxValue - xpSlider.minValue):0.00}% " +
            $"({xpSlider.value} / {xpSlider.maxValue})";

    }

}