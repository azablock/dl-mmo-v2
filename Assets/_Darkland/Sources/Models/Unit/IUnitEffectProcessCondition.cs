using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitEffectProcessCondition {

        public bool CanProcess(GameObject caster);

    }

}