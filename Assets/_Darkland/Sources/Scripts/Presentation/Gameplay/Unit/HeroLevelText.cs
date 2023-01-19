using _Darkland.Sources.Models.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class HeroLevelText : MonoBehaviour {

        [SerializeField]
        private TMP_Text heroLevelText;

        private void Awake() {
            DarklandHero.LocalHeroStarted += DarklandHeroOnLocalHeroStarted;
            DarklandHero.LocalHeroStopped += DarklandHeroOnLocalHeroStopped;
        }

        private void OnDestroy() {
            DarklandHero.LocalHeroStarted -= DarklandHeroOnLocalHeroStarted;
            DarklandHero.LocalHeroStopped -= DarklandHeroOnLocalHeroStopped;
        }

        private void DarklandHeroOnLocalHeroStarted() {
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientLevelChanged += ClientOnLevelChanged;
        }

        private void DarklandHeroOnLocalHeroStopped() {
            DarklandHero.localHero.GetComponent<IXpHolder>().ClientLevelChanged -= ClientOnLevelChanged;
        }

        [Client]
        private void ClientOnLevelChanged(ExperienceLevelChangeEvent evt) => heroLevelText.text = $"{evt.level}";

    }

}