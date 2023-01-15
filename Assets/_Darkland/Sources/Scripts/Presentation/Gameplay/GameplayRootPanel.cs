using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayRootPanel : MonoBehaviour {

        [SerializeField]
        private LocalHeroPanel localHeroPanel;
        [SerializeField]
        private TargetNetIdPanel targetNetIdPanel;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged += OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientCleared += OnClientCleared;
            PlayerInputMessagesProxy.ClientGetHealthStats += Call;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged -= OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientCleared -= OnClientCleared;
            PlayerInputMessagesProxy.ClientGetHealthStats -= Call;
            
            targetNetIdPanel.gameObject.SetActive(false);
        }

        private void Call(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            if (message.statsHolderNetId == DarklandHero.localHero.netId) {
                localHeroPanel.ClientInit(message);
            }
            else {
                targetNetIdPanel.ClientInit(message);
            }
        }

        private void OnClientChanged(NetworkIdentity obj) {
            targetNetIdPanel.gameObject.SetActive(true);
            targetNetIdPanel.OnClientChanged(obj);
        }

        private void OnClientCleared(NetworkIdentity obj) {
            targetNetIdPanel.gameObject.SetActive(false);
            targetNetIdPanel.OnClientCleared(obj);
        }
    }

}