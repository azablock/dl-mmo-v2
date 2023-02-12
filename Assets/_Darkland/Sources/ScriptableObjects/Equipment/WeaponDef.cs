using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEditor;
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


        public string ItemName => Regex.Replace(name, $"/^ {nameof(WeaponDef)}$/", string.Empty);
        public int ItemPrice => itemPrice;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public int AttackRange => weaponRange == WeaponRange.Melee ? 2 : 4;
        public float AttackSpeed => attackSpeed;
        public Sprite Sprite => sprite;
        public EqItemType ItemType => EqItemType.Wearable;
        public WearableSlot WearableItemSlot => WearableSlot.RightHand;
        public WearableType WearableItemType => WearableType.Weapon;
        public List<WearableStatBonus> StatBonuses => statBonuses;

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

    }
    
    [CustomEditor(typeof(WeaponDef))]
//We need to extend the Editor
    public class WeaponEditor : Editor
    {
        //Here we grab a reference to our Weapon SO
        WeaponDef weapon;

        private void OnEnable()
        {
            //target is by default available for you
            //because we inherite Editor
            weapon = target as WeaponDef;
        }

        //Here is the meat of the script
        public override void OnInspectorGUI()
        {
            //Draw whatever we already have in SO definition
            base.OnInspectorGUI();
            //Guard clause
            if (weapon.Sprite == null)
                return;

            //Convert the weaponSprite (see SO script) to Texture
            Texture2D texture = AssetPreview.GetAssetPreview(weapon.Sprite);
            //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
            //This allows us to place the image JUST UNDER our default inspector
            GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
            //Draws the texture where we have defined our Label (empty space)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }

}