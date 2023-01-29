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

        private static List<IEqItemDef> _allItems;

        private void Awake() {
            _allItems = new List<IEqItemDef>();
            _allItems.AddRange(consumables);
            _allItems.AddRange(weapons);
        }
        
        public static IEqItemDef ItemDef2(string itemName) {
            var idx = _allItems.FindIndex(it => itemName.Equals(it.ItemName));
            Assert.IsTrue(idx > -1 && idx < _allItems.Count);
            return _allItems[idx];
        }
        
    }

}