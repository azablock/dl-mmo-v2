using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Scripts.Interaction;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Darkland.Sources.ScriptableObjects.Mob {

    [CreateAssetMenu(fileName = nameof(MobDef), menuName = "DL/" + nameof(MobDef))]
    public class MobDef : ScriptableObject, Models.Mob.IMobDef {

        [SerializeField]
        [Range(1, 100)]
        private int maxHealth;
        [SerializeField]
        private DamageType damageType;
        [SerializeField]
        private Sprite mobSprite;
        [SerializeField]
        [Range(0, 100)]
        private int minDamage;
        [SerializeField]
        [Range(1, 100)]
        private int maxDamage;
        [SerializeField]
        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        private int passivePerceptionRange;
        [SerializeField]
        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        private int attackPerceptionRange;
        [SerializeField]
        [Range(1, 4)]
        private float movementSpeed;
        [SerializeField]
        [Range(0, 1000)]
        private int xpGain;
        [SerializeField]
        [Range(0, 500)]
        private int minGoldGain;
        [SerializeField]
        [Range(0, 500)]
        private int maxGoldGain;
        [SerializeField]
        [Range(1, 15 * 60)]
        private int respawnTime;
        [SerializeField]
        [Range(1, 4)]
        [Tooltip("in script - formula: '1.0f / reactionSpeed', Base reaction speed = 1")]
        private float reactionSpeed;

        public string MobName => Regex.Replace(name, $"/^ {nameof(MobDef)}$/", string.Empty);
        public int MaxHealth => maxHealth;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public DamageType DamageType => damageType;
        public Sprite MobSprite => mobSprite;
        public float ReactionSpeed => reactionSpeed;
        public float AttackPerceptionRange => attackPerceptionRange;
        public float PassivePerceptionRange => passivePerceptionRange;
        public float MovementSpeed => movementSpeed;
        public int XpGain => xpGain;
        public int MinGoldGain => minGoldGain;
        public int MaxGoldGain => maxGoldGain;
        public int RespawnTime => respawnTime;

    }
    
    [CustomEditor(typeof(MobDef))]
    public class MobDefEditor : Editor
    {
        //Here we grab a reference to our Weapon SO
        public MobDef mob;

        private void OnEnable()
        {
            //target is by default available for you
            //because we inherite Editor
            mob = target as MobDef;
        }

        //Here is the meat of the script
        public override void OnInspectorGUI()
        {
            //Draw whatever we already have in SO definition
            base.OnInspectorGUI();
            //Guard clause
            if (mob.MobSprite == null)
                return;

            //Convert the weaponSprite (see SO script) to Texture
            Texture2D texture = AssetPreview.GetAssetPreview(mob.MobSprite);
            //We crate empty space 80x80 (you may need to tweak it to scale better your sprite
            //This allows us to place the image JUST UNDER our default inspector
            GUILayout.Label("", GUILayout.Height(80), GUILayout.Width(80));
            //Draws the texture where we have defined our Label (empty space)
            GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
        }
    }


}