using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.ScriptableObjects.Equipment;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class EqItemsContainer : MonoBehaviour {

        [SerializeField]
        private List<Consumable> consumables;
        [SerializeField]
        private List<WeaponDef> weapons;

        private List<IEqItemDef> _allItems;

        public static EqItemsContainer _;

        private void Awake() {
            _ = this;

            _allItems = new List<IEqItemDef>();
            _allItems.AddRange(consumables);
            _allItems.AddRange(weapons);
        }
        
        public IEqItemDef ItemDef2(string itemName) {
            var idx = _allItems.FindIndex(it => itemName.Equals(it.ItemName));
            Assert.IsTrue(idx > -1 && idx < _allItems.Count);
            return _allItems[idx];
        }
        
    }

}