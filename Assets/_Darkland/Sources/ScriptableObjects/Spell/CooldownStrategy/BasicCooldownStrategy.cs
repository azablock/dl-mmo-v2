using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CooldownStrategy {

    [CreateAssetMenu(fileName = nameof(BasicCooldownStrategy),
                     menuName = "DL/"  + nameof(SpellCooldownStrategy) + "/" + nameof(BasicCooldownStrategy))]
    public class BasicCooldownStrategy : SpellCooldownStrategy {

        public override float Cooldown(ISpell spell, GameObject caster) {
            return spell.BaseCooldown / caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionSpeed).Current;
        }

    }

}