using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit;
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
        [Header("Stats")]
        [SerializeField]
        private TMP_Text healthRegainText;
        [SerializeField]
        private TMP_Text manaRegainText;
        [SerializeField]
        private TMP_Text actionPowerText;
        [SerializeField]
        private TMP_Text actionSpeedText;
        [SerializeField]
        private TMP_Text magicResistanceText;
        [SerializeField]
        private TMP_Text physicalResistanceText;
        [SerializeField]
        private TMP_Text movementSpeedText;
        [Space]
        [Header("Traits Distribution")]
        [SerializeField]
        private TMP_Text pointsToDistributeText;
        [SerializeField]
        private List<TraitDistributionImage> traitDistributionImages;
        
        private Dictionary<StatId, TMP_Text> _statTexts;

        private void Awake() {
            _statTexts = new Dictionary<StatId, TMP_Text> {
                { StatId.Might, mightText },
                { StatId.Constitution, constitutionText},
                { StatId.Dexterity, dexterityText},
                { StatId.Intellect, intellectText}, 
                { StatId.Soul, soulText}, 
                { StatId.HealthRegain, healthRegainText}, 
                { StatId.ManaRegain, manaRegainText}, 
                { StatId.ActionPower, actionPowerText}, 
                { StatId.ActionSpeed, actionSpeedText}, 
                { StatId.MagicResistance, magicResistanceText},
                { StatId.PhysicalResistance, physicalResistanceText}, 
                { StatId.MovementSpeed, movementSpeedText}
            };
        }

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged += OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IXpHolder>().ClientLevelChanged += OnClientLevelChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IHeroTraitDistribution>().ClientChanged += OnTraitDistributionClientChanged;
            DarklandHeroMessagesProxy.ClientGetHeroSheet += ClientOnGetHeroSheet;
            
            var heroName = DarklandHeroBehaviour.localHero.GetComponent<UnitNameBehaviour>().unitName;
            NetworkClient.Send(new DarklandHeroMessages.GetHeroSheetRequestMessage {heroName = heroName});

            //todo fuj
            OnTraitDistributionClientChanged(DarklandHeroBehaviour
                                                 .localHero
                                                 .GetComponent<IHeroTraitDistribution>()
                                                 .pointsToDistribute);
        }
        
        private void OnDisable() {
            DarklandHeroBehaviour.localHero.GetComponent<IStatsHolder>().ClientChanged -= OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IXpHolder>().ClientLevelChanged -= OnClientLevelChanged;
            DarklandHeroBehaviour.localHero.GetComponent<IHeroTraitDistribution>().ClientChanged -= OnTraitDistributionClientChanged;
            DarklandHeroMessagesProxy.ClientGetHeroSheet -= ClientOnGetHeroSheet;
        }

        [Client]
        private void OnClientChanged(StatId statId, StatVal val) {
            if (!_statTexts.ContainsKey(statId)) return;

            _statTexts[statId].text = FormatStatVal(val);
        }

        [Client]
        private void OnClientLevelChanged(ExperienceLevelChangeEvent evt) => heroLevelText.text = $"{evt.level}";

        [Client]
        private void OnTraitDistributionClientChanged(int pointsToDistribute) {
            pointsToDistributeText.text = $"Points to distribute: {pointsToDistribute}";

            var showDistribution = pointsToDistribute > 0;
            
            pointsToDistributeText.gameObject.SetActive(showDistribution);
            traitDistributionImages.ForEach(it => it.gameObject.SetActive(showDistribution));
        }

        
        [Client]
        private void ClientOnGetHeroSheet(DarklandHeroMessages.GetHeroSheetResponseMessage message) {
            heroNameText.text = message.heroName;
            heroLevelText.text = $"{message.heroLevel}";
            heroVocationText.text = message.heroVocationType.ToString();

            mightText.text = FormatStatVal(message.heroTraits.might);
            constitutionText.text = FormatStatVal(message.heroTraits.constitution);
            dexterityText.text = FormatStatVal(message.heroTraits.dexterity);
            intellectText.text = FormatStatVal(message.heroTraits.intellect);
            soulText.text = FormatStatVal(message.heroTraits.soul);
            
            healthRegainText.text = FormatStatVal(message.heroSecondaryStats.healthRegain);
            manaRegainText.text = FormatStatVal(message.heroSecondaryStats.manaRegain);
            actionPowerText.text = FormatStatVal(message.heroSecondaryStats.actionPower);
            actionSpeedText.text = FormatStatVal(message.heroSecondaryStats.actionSpeed);
            magicResistanceText.text = FormatStatVal(message.heroSecondaryStats.magicResistance);
            physicalResistanceText.text = FormatStatVal(message.heroSecondaryStats.physicalResistance);
            movementSpeedText.text = FormatStatVal(message.heroSecondaryStats.movementSpeed);
        }

        private static string FormatStatVal(StatVal val) {
            var bonusSign = val.Bonus >= 0 ? "+" : "-";
            var bonusColor = val.Bonus > 0 ? DarklandColorSet._.success : val.Bonus < 0
                ? DarklandColorSet._.danger
                : DarklandColorSet._.primary;
            var bonusAbsValue = $"{Math.Abs(val.Bonus)}";
            var bonusSuffix = $"{bonusSign} {RichTextFormatter.Colored(bonusAbsValue, bonusColor):F1}";
            var currentVal = $"{val.Current:F1}";
            var currentValueFormatted = $"{RichTextFormatter.Colored(currentVal, bonusColor)}";
            
            return $"<b>{currentValueFormatted}</b>\t= {val.Basic:F1} {bonusSuffix}";
        }

    }

}