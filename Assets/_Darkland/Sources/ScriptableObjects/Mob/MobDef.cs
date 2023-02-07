using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Scripts.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Mob {

    public interface IMobDef {

        string MobName { get; }
        int MaxHealth { get; }
        int MinDamage { get; }
        int MaxDamage { get; }
        DamageType DamageType { get; }
        float PassivePerceptionRange { get; }
        float AttackPerceptionRange { get; }
        float MovementSpeed { get; }
        int XpGain { get; }
        int MinGoldGain { get; }
        int MaxGoldGain { get; }
        int RespawnTime { get; }
    }
    
    [CreateAssetMenu(fileName = nameof(MobDef), menuName = "DL/" + nameof(MobDef))]
    public class MobDef : ScriptableObject, IMobDef {

        [SerializeField]
        [Range(1, 100)]
        private int maxHealth;
        [SerializeField]
        private DamageType damageType;
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
        [Range(0, 4)]
        private float movementSpeed;
        [SerializeField]
        [Range(0, 1000)]
        private int xpGain;
        [SerializeField]
        [Range(0, 100)]
        private int minGoldGain;
        [SerializeField]
        [Range(0, 100)]
        private int maxGoldGain;
        [SerializeField]
        [Range(1, 15 * 60)]
        private int respawnTime;

        public string MobName => Regex.Replace(name, $"/^ {nameof(MobDef)}$/", string.Empty);
        public int MaxHealth => maxHealth;
        public int MinDamage => minDamage;
        public int MaxDamage => maxDamage;
        public DamageType DamageType => damageType;
        public float AttackPerceptionRange => attackPerceptionRange;
        public float PassivePerceptionRange => passivePerceptionRange;
        public float MovementSpeed => movementSpeed;
        public int XpGain => xpGain;
        public int MinGoldGain => minGoldGain;
        public int MaxGoldGain => maxGoldGain;
        public int RespawnTime => respawnTime;

    }

}