using System;
using _Darkland.Sources.Scripts.Unit.Combat;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Models.Combat {

    public interface IDamageDealer {

        void DealDamage(UnitAttackEvent evt);
        
        //todo nextAttackDamageBonus - raczej slaby pomysl...
        void AddNextAttackDamageBonus(int val);
        int nextAttackDamageBonus { get; }

        event Action<DamageAppliedEvent> ServerDamageApplied;

        public const int UnarmedAttackRange = 2;
        public static int UnarmedBaseDamageRange() => Random.Range(1, 3);

    }

}