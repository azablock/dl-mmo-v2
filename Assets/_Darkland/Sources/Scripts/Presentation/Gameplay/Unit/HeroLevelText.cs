using _Darkland.Sources.Models.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class HeroLevelText : MonoBehaviour {

        [SerializeField]
        private TMP_Text heroLevelText;

        private void Awake() {
            DarklandHeroBehaviour.LocalHeroStarted += DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped += DarklandHeroOnLocalHeroStopped;
        }

        private void OnDestroy() {
            DarklandHeroBehaviour.LocalHeroStarted -= DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped -= DarklandHeroOnLocalHeroStopped;
        }

        private void DarklandHeroOnLocalHeroStarted() {
            DarklandHeroBehaviour.localHero.GetComponent<IXpHolder>().ClientLevelChanged += ClientOnLevelChanged;
        }

        private void DarklandHeroOnLocalHeroStopped() {
            DarklandHeroBehaviour.localHero.GetComponent<IXpHolder>().ClientLevelChanged -= ClientOnLevelChanged;
        }

        [Client]
        private void ClientOnLevelChanged(ExperienceLevelChangeEvent evt) => heroLevelText.text = $"{evt.level}";

    }

}