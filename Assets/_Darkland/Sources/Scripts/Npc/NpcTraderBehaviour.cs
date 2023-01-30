using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.ScriptableObjects.Equipment;
using _Darkland.Sources.Scripts.Presentation.Gameplay;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Npc {

    public class NpcTraderBehaviour : MonoBehaviour {

        [SerializeField]
        private List<WeaponDef> weapons;
        [SerializeField]
        private List<Consumable> consumables;

        private List<IEqItemDef> _allItems;

        private void Awake() {
            _allItems = new List<IEqItemDef>();
            _allItems.AddRange(weapons);
            _allItems.AddRange(consumables);
        }

        [Client]
        public void ClientToggle() {
            var tradePanel = GameplayRootPanel.TradeRootPanel;
            
            //todo tmp switch
            if (tradePanel.gameObject.activeSelf) {
                tradePanel.gameObject.SetActive(false);
            }
            else {
                tradePanel.gameObject.SetActive(true);
                tradePanel.TradeItemsPanel.ClientSet(_allItems);
            }
        }

    }

}