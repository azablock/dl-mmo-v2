using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Npc;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeItemsPanel : MonoBehaviour {

        [SerializeField]
        private GameObject tradeItemViewPrefab;
        [SerializeField]
        private TMP_Text traderNameText;
        [SerializeField]
        private TMP_Text traderInfoText;

        public static event Action<bool> Toggled;

        public NpcTraderBehaviour npcTrader { get; private set; }

        [Client]
        public void ClientSet(NpcTraderBehaviour trader, List<IEqItemDef> items) {
            npcTrader = trader;
            traderNameText.text = $"{npcTrader.traderName}";
            traderInfoText.text = $"{npcTrader.traderInfo}";

            items
                .OrderBy(it => it.ItemPrice)
                .ToList()
                .ForEach(it =>
                             Instantiate(tradeItemViewPrefab, transform)
                                 .GetComponent<TradeItemView>()
                                 .ClientSet(it));

            Toggled?.Invoke(true);

            ClientRefreshPricePoints(DarklandHeroBehaviour.localHero.GetComponent<IGoldHolder>().GoldAmount);
        }

        [Client]
        public void ClientClear() {
            foreach (Transform child in transform) Destroy(child.gameObject);
            Toggled?.Invoke(false);
            npcTrader = null;
        }

        [Client]
        public void ClientRefreshPricePoints(int goldAmount) {
            GetComponentsInChildren<TradeItemView>()
                .ToList()
                .ForEach(it => it.ClientRefreshPricePoint(goldAmount));
        }

    }

}