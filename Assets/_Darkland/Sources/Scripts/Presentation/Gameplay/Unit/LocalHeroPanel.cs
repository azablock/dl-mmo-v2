using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class LocalHeroPanel : MonoBehaviour {

        [SerializeField]
        private DarklandUnitInfoPanel localHeroInfoPanel;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged += ClientOnStatsChanged;
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnPosChanged;
            ClientOnPosChanged(DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos);
            
            NetworkClient.Send(new PlayerInputMessages.GetHealthStatsRequestMessage {statsHolderNetId = DarklandHero.localHero.netId});
        }
        
        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged -= ClientOnStatsChanged;
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnPosChanged;
        }

        [Client]
        public void ClientInit(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            localHeroInfoPanel.ClientSetUnitName(message.unitName);
            localHeroInfoPanel.ClientSetMaxHealth(message.maxHealth);
            localHeroInfoPanel.ClientSetHealth(message.health);
        }

        [Client]
        private void ClientOnStatsChanged(StatId statId, float val) {
            if (statId == StatId.Health) {
                localHeroInfoPanel.ClientSetHealth(val);
            }
            else if (statId == StatId.MaxHealth) {
                localHeroInfoPanel.ClientSetMaxHealth(val);
            }
        }
        
        [Client]
        private void ClientOnPosChanged(Vector3Int pos) {
            //todo
            // localPlayerPosText.text = $"Pos {pos}";
        }


    }


}