using System;
using _Darkland.Sources.Scripts.Unit.Combat;

namespace _Darkland.Sources.Models.Combat {

    public interface IDamageDealer {

        void DealDamage(UnitAttackEvent evt);

        event Action<UnitAttackEvent> ServerDamageApplied;

    }

}