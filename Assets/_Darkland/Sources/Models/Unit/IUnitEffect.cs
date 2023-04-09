using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitEffect {

        void PreProcess(GameObject effectHolder);
        IEnumerator Process(GameObject effectHolder);
        void PostProcess(GameObject effectHolder);
        string Description(GameObject parent);
        
        float Duration { get; }
        string EffectName { get; }
        Sprite EffectSprite { get; }
        List<IUnitEffectProcessCondition> EffectProcessConditions { get; }

    }

}