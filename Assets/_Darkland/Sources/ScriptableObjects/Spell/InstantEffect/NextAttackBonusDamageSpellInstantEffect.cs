using _Darkland.Sources.Models.Combat;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(NextAttackBonusDamageSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(NextAttackBonusDamageSpellInstantEffect))]
    public class NextAttackBonusDamageSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private int damageBonus;
        
        public override void Process(GameObject caster) {
            caster.GetComponent<IDamageDealer>().AddNextAttackDamageBonus(damageBonus);
        }

    }

}