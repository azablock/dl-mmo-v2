using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEditor;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    public abstract class Consumable : ScriptableObject, IConsumable, IEqItemDef {

        // [SerializeField]
        // protected string itemName;
        [SerializeField]
        protected int itemPrice;
        [SerializeField]
        protected Sprite sprite;

        public abstract void Consume(GameObject eqHolder);
        public abstract string Description(GameObject parent);

        public string ItemName => Regex.Replace(name, $"/^ {nameof(Consumable)}$/", string.Empty);
        public int ItemPrice => itemPrice;
        public EqItemType ItemType => EqItemType.Consumable;
        public Sprite Sprite => sprite;


    }

}