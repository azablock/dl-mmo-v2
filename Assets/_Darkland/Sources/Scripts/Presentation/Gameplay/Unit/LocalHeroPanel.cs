using System.Collections.Generic;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit;
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
        [SerializeField]
        private UnitEffectsPanel unitEffectsPanel;

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged += ClientOnStatsChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnPosChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>().ClientNotified +=
                OnClientNotified;

            ClientOnPosChanged(DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().Pos);

            NetworkClient.Send(new PlayerInputMessages.GetHealthStatsRequestMessage
                                   { statsHolderNetId = DarklandHeroBehaviour.localHero.netId });
        }

        private void OnDisable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged -= ClientOnStatsChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnPosChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>().ClientNotified -=
                OnClientNotified;
        }

        [Client]
        public void ClientInit(PlayerInputMessages.GetHealthStatsResponseMessage message) {
            localHeroInfoPanel.ClientSetUnitName(message.unitName);
            localHeroInfoPanel.ClientSetMaxHealth(message.maxHealth);
            localHeroInfoPanel.ClientSetHealth(message.health);
            localHeroInfoPanel.ClientSetMaxMana(message.maxMana);
            localHeroInfoPanel.ClientSetMana(message.mana);
        }

        [Client]
        private void ClientOnStatsChanged(StatId statId, StatVal val) {
            if (statId == StatId.Health)
                localHeroInfoPanel.ClientSetHealth(val.Basic);
            else if (statId == StatId.MaxHealth)
                localHeroInfoPanel.ClientSetMaxHealth(val.Current);
            else if (statId == StatId.Mana)
                localHeroInfoPanel.ClientSetMana(val.Basic);
            else if (statId == StatId.MaxMana) localHeroInfoPanel.ClientSetMaxMana(val.Current);
        }

        [Client]
        private void ClientOnPosChanged(Vector3Int pos) {
            positionText.text = $"{pos}";
        }

        private void OnClientNotified(List<string> effectNames) {
            unitEffectsPanel.ClientRefreshUnitEffects(effectNames);
        }

    }

}