using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(NextAttackBonusDamageSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(NextAttackBonusDamageSpellInstantEffect))]
    public class NextAttackBonusDamageSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private int damageBonus;
        
        public override void Process(GameObject caster) {
            var actionPower = Mathf.FloorToInt(caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current);
            caster.GetComponent<IDamageDealer>().AddNextAttackDamageBonus(damageBonus + actionPower);
        }

        public override string Description(GameObject caster) {
            var actionPower = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current;
            return $"Attack with extra {damageBonus + actionPower} damage.";
        }

    }

}