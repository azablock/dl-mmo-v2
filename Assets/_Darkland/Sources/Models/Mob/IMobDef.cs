using _Darkland.Sources.Models.Combat;
using UnityEngine;

namespace _Darkland.Sources.Models.Mob {

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
        Sprite MobSprite { get; }
        //todo tmp
        float ReactionSpeed { get; }
        float HealthRegain { get; }
    }

}