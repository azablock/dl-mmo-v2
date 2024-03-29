using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    public enum WeaponRange {

        Melee,
        Ranged

    }

    [CreateAssetMenu(fileName = nameof(WeaponDef),
                     menuName = "DL/Eq/" + nameof(WeaponDef))]
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
        private float attackSpeed;
        [SerializeField]
        private WeaponRange weaponRange;
        [SerializeField]
        private List<WearableStatBonus> statBonuses;


        public string ItemName => Regex.Replace(name, $" {nameof(WeaponDef)}", string.Empty);
        public int ItemPrice => itemPrice;
        public Sprite Sprite => sprite;
        public EqItemType ItemType => EqItemType.Wearable;

        public string Description(GameObject parent) {
            var bonuses = statBonuses.Aggregate(string.Empty, (res, bonus) => {
                var signStr = bonus.buffValue > 0 ? "+" : "-";
                return res + $"{bonus.statId.ToString()}\t{signStr} {bonus.buffValue}\n";
            });

            return $"Weapon Damage:\t{MinDamage} - {MaxDamage}\n" +
                   $"Attack Range:\t{AttackRange}\n" +
                   $"Attack Speed:\t{AttackSpeed}\n\n" +
                   $"{bonuses}";
        }

        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public int AttackRange => weaponRange == WeaponRange.Melee ? 2 : 4;
        public float AttackSpeed => attackSpeed;
        public WearableSlot WearableItemSlot => WearableSlot.RightHand;
        public WearableType WearableItemType => WearableType.Weapon;
        public List<WearableStatBonus> StatBonuses => statBonuses;

    }

}