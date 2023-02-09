using System;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Trade;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayRootPanel : MonoBehaviour {

        [SerializeField]
        private LocalHeroPanel localHeroPanel;
        [SerializeField]
        private TargetNetIdPanel targetNetIdPanel;
        [SerializeField]
        private TradeRootPanel tradeRootPanel;

        public static GameplayRootPanel _;
        
        private void Awake() {
            _ = this;
        }

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged += OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared += OnClientCleared;
            
            PlayerInputMessagesProxy.ClientGetHealthStats += Call;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged -= OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared -= OnClientCleared;
            PlayerInputMessagesProxy.ClientGetHealthStats -= Call;
            
            targetNetIdPanel.gameObject.SetActive(false);
        }

        private void Call(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            if (message.statsHolderNetId == DarklandHeroBehaviour.localHero.netId) {
                localHeroPanel.ClientInit(message);
            }
            
            targetNetIdPanel.ClientInit(message);

            // else {
                // targetNetIdPanel.ClientInit(message);
            // }
        }

        private void OnClientChanged(NetworkIdentity obj) {
            targetNetIdPanel.gameObject.SetActive(true);
            targetNetIdPanel.OnClientChanged(obj);
        }

        private void OnClientCleared(NetworkIdentity obj) {
            targetNetIdPanel.OnClientCleared(obj);
            targetNetIdPanel.gameObject.SetActive(false);
        }

        public static TradeRootPanel TradeRootPanel => _.tradeRootPanel;

    }

}