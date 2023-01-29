using _Darkland.Sources.Models.Equipment;
using UnityEngine;
using UnityEngine.Serialization;

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

        public string ItemName => name;
        public int ItemPrice => itemPrice;
        public int MinDamage => minDamage;
        public EqItemType ItemType => EqItemType.Wearable;
        public Sprite Sprite => sprite;
        public WearableSlot WearableItemSlot => WearableSlot.RightHand;
        public WearableType WearableItemType => WearableType.Weapon;

    }

}