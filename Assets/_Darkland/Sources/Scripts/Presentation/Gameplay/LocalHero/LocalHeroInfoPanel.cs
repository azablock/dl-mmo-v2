using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class LocalHeroInfoPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text localHeroNameText;
        [SerializeField]
        private Slider localHeroHealthSlider;
        [SerializeField]
        private TMP_Text localHeroHealthText;
        
        private void OnEnable() {
            localHeroNameText.text = DarklandHero.localHero.heroName;
            
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged += ClientOnStatsChanged;
            PlayerInputMessagesProxy.ClientGetHealthStats += ClientInitHealthSlider;
            
            NetworkClient.Send(new PlayerInputMessages.GetHealthStatsRequestMessage());
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged -= ClientOnStatsChanged;
            PlayerInputMessagesProxy.ClientGetHealthStats -= ClientInitHealthSlider;
        }

        private void ClientInitHealthSlider(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            localHeroHealthSlider.maxValue = message.maxHealth;
            localHeroHealthSlider.value = message.health;

            ClientUpdateHeroHealthText();
        }

        [Client]
        private void ClientOnStatsChanged(StatId statId, float statValue) {
            if (statId == StatId.Health) {
                localHeroHealthSlider.value = statValue;
            }
            else if (statId == StatId.MaxHealth) {
                localHeroHealthSlider.maxValue = statValue;
            }
        }

        [Client]
        private void ClientUpdateHeroHealthText() {
            localHeroHealthText.text = $"{localHeroHealthSlider.value} / {localHeroHealthSlider.maxValue}";
        }
    }
}