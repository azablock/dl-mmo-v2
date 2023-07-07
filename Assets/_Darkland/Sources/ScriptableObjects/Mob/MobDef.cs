using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Scripts.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Mob {

    [CreateAssetMenu(fileName = nameof(MobDef), menuName = "DL/" + nameof(MobDef))]
    public class MobDef : ScriptableObject, IMobDef {

        [SerializeField]
        [Range(1, 300)]
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
        [SerializeField]
        [Range(0, 10)]
        private float healthRegain;

        public string MobName => Regex.Replace(name, $" {nameof(MobDef)}", string.Empty);
        public int MaxHealth => maxHealth;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public DamageType DamageType => damageType;
        public Sprite MobSprite => mobSprite;
        public float ReactionSpeed => reactionSpeed;
        public float HealthRegain => healthRegain;
        public float AttackPerceptionRange => attackPerceptionRange;
        public float PassivePerceptionRange => passivePerceptionRange;
        public float MovementSpeed => movementSpeed;
        public int XpGain => xpGain;
        public int MinGoldGain => minGoldGain;
        public int MaxGoldGain => maxGoldGain;
        public int RespawnTime => respawnTime;

    }

}