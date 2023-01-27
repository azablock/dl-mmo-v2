using System.Collections.Generic;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.ScriptableObjects.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(TargetUnitEffectSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(TargetUnitEffectSpellInstantEffect))]
    public class TargetUnitEffectSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private List<UnitEffect> unitEffects;

        public override void Process(GameObject caster) {
            unitEffects.ForEach(it => caster
                                    .GetComponent<ITargetNetIdHolder>()
                                    .TargetNetIdentity
                                    .GetComponent<IUnitEffectHolder>()
                                    .ServerAdd(it));
        }

    }

}