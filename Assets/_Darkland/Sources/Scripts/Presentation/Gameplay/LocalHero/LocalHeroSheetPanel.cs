using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class LocalHeroSheetPanel : MonoBehaviour {

        [Header("Basic Info")]
        [SerializeField]
        private TMP_Text heroNameText;
        [SerializeField]
        private TMP_Text heroLevelText;
        [SerializeField]
        private TMP_Text heroVocationText;
        [Header("Traits")]
        [SerializeField]
        private TMP_Text mightText;
        [SerializeField]
        private TMP_Text constitutionText;
        [SerializeField]
        private TMP_Text dexterityText;
        [SerializeField]
        private TMP_Text intellectText;
        [SerializeField]
        private TMP_Text soulText;

        private readonly Dictionary<StatId, Action<float, LocalHeroSheetPanel>> _statChangedCallbacks = new() {
            { StatId.Might, (val, panel) => panel.mightText.text = $"{val}" },
            { StatId.Constitution, (val, panel) => panel.constitutionText.text = $"{val}" },
            { StatId.Dexterity, (val, panel) => panel.dexterityText.text = $"{val}" },
            { StatId.Intellect, (val, panel) => panel.intellectText.text = $"{val}" },
            { StatId.Soul, (val, panel) => panel.soulText.text = $"{val}" },
        };

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged += OnClientChanged;
            DarklandHeroMessagesProxy.ClientGetHeroSheet += ClientOnGetHeroSheet;
            
            var heroName = DarklandHero.localHero.GetComponent<UnitNameBehaviour>().unitName;
            NetworkClient.Send(new DarklandHeroMessages.GetHeroSheetRequestMessage {heroName = heroName});
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged -= OnClientChanged;
        }

        [Client]
        private void OnClientChanged(StatId statId, float val) {
            if (!_statChangedCallbacks.ContainsKey(statId)) return;

            _statChangedCallbacks[statId].Invoke(val, this);
        }

        [Client]
        private void ClientOnGetHeroSheet(DarklandHeroMessages.GetHeroSheetResponseMessage message) {
            heroNameText.text = message.heroName;
            heroLevelText.text = $"{message.heroLevel}";
            heroVocationText.text = message.heroVocation.ToString();
            mightText.text = $"{message.heroTraits.might}";
            constitutionText.text = $"{message.heroTraits.constitution}";
            dexterityText.text = $"{message.heroTraits.dexterity}";
            intellectText.text = $"{message.heroTraits.intellect}";
            soulText.text = $"{message.heroTraits.soul}";
        }

    }

}