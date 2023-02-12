using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Equipment;
using UnityEditor;
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

        public string ItemName => Regex.Replace(name, $"/^ {nameof(BasicWearableDef)}$/", string.Empty);
        public int ItemPrice => itemPrice;
        public Sprite Sprite => sprite;
        public EqItemType ItemType => EqItemType.Wearable;
        public WearableSlot WearableItemSlot => wearableItemSlot;
        public WearableType WearableItemType => WearableType.Weapon;
        public List<WearableStatBonus> StatBonuses => statBonuses;

        public string Description(GameObject parent) {
            var bonuses = statBonuses.Aggregate(string.Empty, (res, bonus) => {
                var signStr = bonus.buffValue > 0 ? "+" : "-";
                return res + $"{bonus.statId.ToString()}\t{signStr} {bonus.buffValue}\n";
            });

            return $"Wearable slot:\t{wearableItemSlot.ToString()}\n{bonuses}";
        }

    }
    
    [CustomEditor(typeof(BasicWearableDef))]
//We need to extend the Editor
    public class EqItemEditor : Editor
    {
        //Here we grab a reference to our Weapon SO
        public BasicWearableDef eqItem;

        private void OnEnable()
        {
            //target is by default available for you
            //because we inherite Editor
            eqItem = target as BasicWearableDef;
        }

        //Here is the meat of the script
        public override void OnInspectorGUI()
        {
            //Draw whatever we already have in SO definition
            base.OnInspectorGUI();
            //Guard clause
            if (eqItem.Sprite == null)
                return;

            //Convert the weaponSprite (see SO script) to Texture
            Texture2D texture = AssetPreview.GetAssetPreview(eqItem.Sprite);
            //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
            //This allows us to place the image JUST UNDER our default inspector
            GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
            //Draws the texture where we have defined our Label (empty space)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }


}