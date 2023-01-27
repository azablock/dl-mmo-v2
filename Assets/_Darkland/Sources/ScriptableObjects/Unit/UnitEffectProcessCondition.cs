using _Darkland.Sources.Models.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Unit {

    public abstract class UnitEffectProcessCondition : ScriptableObject, IUnitEffectProcessCondition {

        public abstract bool CanProcess(GameObject caster);

    }

}