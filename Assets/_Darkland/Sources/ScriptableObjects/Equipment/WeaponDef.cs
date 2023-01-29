using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(WeaponDef),
                     menuName = "DL/Eq/"  + nameof(WeaponDef))]
    public class WeaponDef : ScriptableObject, IWeaponDef, IEqItemDef, IWearable {

        // [SerializeField]
        // private string itemName;
        [SerializeField]
        private int itemPrice;
        [Header("Weapon Stats")]
        [SerializeField]
        private int minDamage;
        [SerializeField]
        private Sprite sprite;

        public string ItemName => Regex.Replace(name, $"/^ {nameof(WeaponDef)}$/", string.Empty);
        public int ItemPrice => itemPrice;
        public int MinDamage => minDamage;
        public EqItemType ItemType => EqItemType.Wearable;
        public Sprite Sprite => sprite;
        public WearableSlot WearableItemSlot => WearableSlot.RightHand;
        public WearableType WearableItemType => WearableType.Weapon;

    }

}