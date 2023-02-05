using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class LocalHeroPanel : MonoBehaviour {

        [SerializeField]
        private DarklandUnitInfoPanel localHeroInfoPanel;

        [SerializeField]
        private TMP_Text positionText;

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged += ClientOnStatsChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnPosChanged;
            ClientOnPosChanged(DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().Pos);
            
            NetworkClient.Send(new PlayerInputMessages.GetHealthStatsRequestMessage {statsHolderNetId = DarklandHeroBehaviour.localHero.netId});
        }
        
        private void OnDisable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged -= ClientOnStatsChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnPosChanged;
        }

        [Client]
        public void ClientInit(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            localHeroInfoPanel.ClientSetUnitName(message.unitName);
            localHeroInfoPanel.ClientSetMaxHealth(message.maxHealth);
            localHeroInfoPanel.ClientSetHealth(message.health);
        }

        [Client]
        private void ClientOnStatsChanged(StatId statId, StatVal val) {
            if (statId == StatId.Health) {
                localHeroInfoPanel.ClientSetHealth(val.Basic);
            }
            else if (statId == StatId.MaxHealth) {
                localHeroInfoPanel.ClientSetMaxHealth(val.Current);
            }
        }

        [Client]
        private void ClientOnPosChanged(Vector3Int pos) => positionText.text = $"{pos}";


    }


}