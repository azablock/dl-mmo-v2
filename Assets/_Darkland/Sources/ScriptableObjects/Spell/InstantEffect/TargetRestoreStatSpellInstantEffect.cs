using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(TargetRestoreStatSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(TargetRestoreStatSpellInstantEffect))]
    public class TargetRestoreStatSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private StatId restoreStatId;
        [SerializeField]
        private int restoreAmount;
        
        public override void Process(GameObject caster) {
            var target = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            target.GetComponent<IStatsHolder>().Add(restoreStatId, StatVal.OfBasic(restoreAmount));
        }

    }

}