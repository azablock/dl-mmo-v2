using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class TargetNetIdPanel : MonoBehaviour {

        [SerializeField]
        private DarklandUnitInfoPanel targetNetIdentityPanel;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged += OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared += OnClientCleared;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged -= OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared -= OnClientCleared;
        }
        
        public void ClientInit(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            targetNetIdentityPanel.ClientSetUnitName(message.unitName);
            targetNetIdentityPanel.ClientSetMaxHealth(message.maxHealth);
            targetNetIdentityPanel.ClientSetHealth(message.health);
        }

        public void OnClientChanged(NetworkIdentity targetNetIdentity) {
            NetworkClient.Send(new PlayerInputMessages.GetHealthStatsRequestMessage {statsHolderNetId = targetNetIdentity.netId});
            
            targetNetIdentity.GetComponent<IStatsHolder>().ClientChanged += OnClientStatsChanged;
        }

        public void OnClientCleared(NetworkIdentity targetNetIdentity) {
            //todo chyba trzeba to sprawdzic - bo na serwrze ten identity juz moze nie istniec?
            if (targetNetIdentity != null) {
                targetNetIdentity.GetComponent<IStatsHolder>().ClientChanged -= OnClientStatsChanged;
            }
        }

        private void OnClientStatsChanged(StatId statId, float val) {
            if (statId == StatId.Health) {
                targetNetIdentityPanel.ClientSetHealth(val);
            }
            else if (statId == StatId.MaxHealth) {
                targetNetIdentityPanel.ClientSetMaxHealth(val);
            }
        }

    }

}