using System.Collections.Generic;
using _Darkland.Sources.Models.Core;
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
        private List<BasicWearableDef> basicWearables;
        [SerializeField]
        private List<Consumable> consumables;

        private List<IEqItemDef> _allItems;
        [SerializeField]
        private float maxVisibleDistance;
        [SerializeField]
        public string traderName;
        [SerializeField]
        [TextArea]
        public string traderInfo;

        private void Awake() {
            _allItems = new List<IEqItemDef>();
            _allItems.AddRange(weapons);
            _allItems.AddRange(basicWearables);
            _allItems.AddRange(consumables);
        }
        
        private void FixedUpdate() {
            if (DarklandHeroBehaviour.localHero == null) return;
            var localPlayerPos = DarklandHeroBehaviour.localHero.GetComponent<IDiscretePosition>().Pos;

            //todo fuj
            if (!LocalPlayerInProximity(localPlayerPos) && Equals(GameplayRootPanel.TradeRootPanel.TradeItemsPanel.npcTrader)) Hide();
        }

        private static void Hide() {
            var tradePanel = GameplayRootPanel.TradeRootPanel;
            tradePanel.TradeItemsPanel.ClientClear();
            tradePanel.gameObject.SetActive(false);
        }


        [Client]
        public void ClientToggle() {
            var tradePanel = GameplayRootPanel.TradeRootPanel;
            
            //todo tmp switch
            if (tradePanel.gameObject.activeSelf) {
                Hide();
            }
            else {
                tradePanel.gameObject.SetActive(true);
                tradePanel.TradeItemsPanel.ClientSet(this, _allItems);
            }
        }

        private bool LocalPlayerInProximity(Vector3Int localPlayerPos) {
            var transformPosition = transform.position;
            var inDistance = Vector3.Distance(localPlayerPos, transformPosition) < maxVisibleDistance;
            var zEqual = (int) transformPosition.z == localPlayerPos.z;
            
            return inDistance && zEqual;
        }
        
    }

}