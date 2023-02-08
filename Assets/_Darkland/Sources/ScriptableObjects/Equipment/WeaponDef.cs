using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(WeaponDef),
                     menuName = "DL/Eq/"  + nameof(WeaponDef))]
    public class WeaponDef : ScriptableObject, IWeaponDef, IEqItemDef, IWearable {

        [Header("Eq Item Stats")]
        [SerializeField]
        private int itemPrice;
        [SerializeField]
        private Sprite sprite;
        [Header("Weapon Stats")]
        [SerializeField]
        private int minDamage;
        [SerializeField]
        private int maxDamage;
        [SerializeField]
        private int attackRange;

        public string ItemName => Regex.Replace(name, $"/^ {nameof(WeaponDef)}$/", string.Empty);
        public int ItemPrice => itemPrice;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public int AttackRange => attackRange;
        public Sprite Sprite => sprite;
        public EqItemType ItemType => EqItemType.Wearable;
        public WearableSlot WearableItemSlot => WearableSlot.RightHand;
        public WearableType WearableItemType => WearableType.Weapon;
        
        public string Description(GameObject parent) {
            return $"Damage:\t{MinDamage} - {MaxDamage}\n" +
                   $"Range:\t{AttackRange}\n";
        }
    }

}