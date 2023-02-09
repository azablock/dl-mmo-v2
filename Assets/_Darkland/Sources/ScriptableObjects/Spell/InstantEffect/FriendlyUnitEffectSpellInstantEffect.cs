using System.Collections.Generic;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.ScriptableObjects.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(FriendlyUnitEffectSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(FriendlyUnitEffectSpellInstantEffect))]
    public class FriendlyUnitEffectSpellInstantEffect : SpellInstantEffect {

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