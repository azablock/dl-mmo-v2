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

        public static EqItemsContainer _;

        private void Awake() => _ = this;

        public IEqItemDef ItemDef(string itemName, EqItemType itemType) {
            List<IEqItemDef> items = itemType == EqItemType.Consumable
                ? new(consumables)
                : new(weapons);
            
            var idx = items.FindIndex(it => it.ItemName.Equals(itemName));
            Assert.IsTrue(idx > -1 && idx < items.Count);
            return items[idx];
        }
        
    }

}