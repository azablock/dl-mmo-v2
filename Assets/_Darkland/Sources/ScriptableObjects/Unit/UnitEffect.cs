using System.Collections;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Unit {

    public abstract class UnitEffect : ScriptableObject, IUnitEffect {

        [SerializeField]
        protected string effectName;
        [SerializeField]
        protected List<UnitEffectProcessCondition> effectProcessConditions;
        [SerializeField]
        protected float duration;

        public virtual void PreProcess(GameObject effectHolder) { }
        public abstract IEnumerator Process(GameObject effectHolder);
        public virtual void PostProcess(GameObject effectHolder) { }

        public float Duration => duration;
        public string EffectName => effectName;
        public List<IUnitEffectProcessCondition> EffectProcessConditions => new(effectProcessConditions);

    }

}