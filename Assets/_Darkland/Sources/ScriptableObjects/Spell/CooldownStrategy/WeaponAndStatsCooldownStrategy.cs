using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CooldownStrategy {

    [CreateAssetMenu(fileName = nameof(WeaponAndStatsCooldownStrategy),
                     menuName = "DL/" + nameof(SpellCooldownStrategy) + "/" + nameof(WeaponAndStatsCooldownStrategy))]
    public class WeaponAndStatsCooldownStrategy : SpellCooldownStrategy {

        public override float Cooldown(ISpell spell, GameObject caster) {
            var casterActionSpeed = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionSpeed).Current;
            var equippedWeapon = caster.GetComponent<IEqHolder>().ServerEquippedWeapon();
            var weaponAttackSpeed = equippedWeapon?.AttackSpeed ?? 1.0f;

            return spell.BaseCooldown / (casterActionSpeed * weaponAttackSpeed);
        }

    }

}