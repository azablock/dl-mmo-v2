using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.ScriptableObjects.Equipment;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Trade;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Npc {

    public class NpcTraderBehaviour : MonoBehaviour {

        [SerializeField]
        private List<WeaponDef> weapons;
        [SerializeField]
        private List<Consumable> consumables;

        private List<IEqItemDef> _allItems;

        private TradePanel _tradePanel;

        private void Awake() {
            _allItems = new List<IEqItemDef>();
            _allItems.AddRange(weapons);
            _allItems.AddRange(consumables);

            _tradePanel = FindObjectOfType<TradePanel>();
        }

        [Client]
        public void ClientToggle() {
            //todo tmp switch
            if (_tradePanel.gameObject.activeSelf) {
                _tradePanel.gameObject.SetActive(false);
            }
            else {
                _tradePanel.gameObject.SetActive(true);
                _tradePanel.ClientSet(_allItems);
            }
        }

    }

}