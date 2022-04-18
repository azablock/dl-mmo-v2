using _Darkland.Sources.Models.Unit.Hp;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Ability {

    public struct DirectDamage {
        public int damage;
        public IHpHolder targetHpHolder;
    }

    public struct RangedAction {
        public Vector3Int fromPosition;
        public Vector3Int toPosition;
        public int maxRange;
    }

    public struct FireballData {
        public DirectDamage directDamage;
        public RangedAction rangedAction;
    }

}