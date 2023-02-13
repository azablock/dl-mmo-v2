using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(BasicWearableDef), menuName = "DL/Eq/" + nameof(BasicWearableDef))]
    public class BasicWearableDef : ScriptableObject, IEqItemDef, IWearable {

        [Header("Eq Item Stats")]
        [SerializeField]
        private int itemPrice;
        [SerializeField]
        private Sprite sprite;
        [SerializeField]
        private WearableSlot wearableItemSlot;
        [SerializeField]
        private List<WearableStatBonus> statBonuses;

        public string ItemName => Regex.Replace(name, $" {nameof(BasicWearableDef)}", string.Empty);
        public int ItemPrice => itemPrice;
        public Sprite Sprite => sprite;
        public EqItemType ItemType => EqItemType.Wearable;
        public WearableSlot WearableItemSlot => wearableItemSlot;
        public WearableType WearableItemType => WearableType.Armor;
        public List<WearableStatBonus> StatBonuses => statBonuses;

        public string Description(GameObject parent) {
            var bonuses = statBonuses.Aggregate(string.Empty, (res, bonus) => {
                var signStr = bonus.buffValue > 0 ? "+" : "-";
                return res + $"{bonus.statId.ToString()}\t{signStr} {bonus.buffValue}\n";
            });

            return $"Wearable slot:\t{wearableItemSlot.ToString()}\n{bonuses}";
        }

    }

}