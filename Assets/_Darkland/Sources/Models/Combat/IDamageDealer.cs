using System;
using _Darkland.Sources.Scripts.Unit.Combat;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Models.Combat {

    public interface IDamageDealer {

        void DealDamage(UnitAttackEvent evt);

        event Action<UnitAttackEvent> ServerDamageApplied;

        public const int UnarmedAttackRange = 1;
        public static int UnarmedBaseDamageRange() => Random.Range(1, 2);

    }

}