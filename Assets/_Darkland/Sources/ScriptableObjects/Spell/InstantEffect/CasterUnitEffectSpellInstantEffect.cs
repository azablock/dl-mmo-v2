using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.ScriptableObjects.Unit;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(CasterUnitEffectSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(CasterUnitEffectSpellInstantEffect))]
    public class CasterUnitEffectSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private List<UnitEffect> unitEffects;

        public override void Process(GameObject caster) {
            unitEffects.ForEach(it => caster.GetComponent<IUnitEffectHolder>().ServerAdd(it));
        }

        public override string Description(GameObject caster) {
            var descriptions = unitEffects
                .Select(it => it.Description(caster))
                .Aggregate(string.Empty, (desc, next) => desc + next + "\n");

            return $"Works on caster.\n{descriptions}";
        }

    }

}