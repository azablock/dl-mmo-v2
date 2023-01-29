using _Darkland.Sources.Models.Equipment;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    public abstract class Consumable : ScriptableObject, IConsumable, IEqItemDef {

        // [SerializeField]
        // protected string itemName;
        [SerializeField]
        protected int itemPrice;
        [SerializeField]
        protected Sprite sprite;

        public abstract void Consume(GameObject eqHolder);

        public string ItemName => name;
        public int ItemPrice => itemPrice;
        public EqItemType ItemType => EqItemType.Consumable;
        public Sprite Sprite => sprite;

    }

}