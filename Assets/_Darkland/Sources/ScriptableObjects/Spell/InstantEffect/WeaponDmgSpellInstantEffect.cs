using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.ScriptableObjects.Stats2;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(WeaponDmgSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(WeaponDmgSpellInstantEffect))]
    public class WeaponDmgSpellInstantEffect : SpellInstantEffect {

        public override void Process(GameObject caster) {
            var targetNetIdentity = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var statsHolder = caster.GetComponent<IStatsHolder>();
            var weapon = caster.GetComponent<IEqHolder>().ServerEquippedWeapon();
            var weaponDamage = weapon != null
                ? Random.Range(weapon.MinDamage, weapon.MinDamage)
                : IDamageDealer.UnarmedBaseDamageRange();
            var actionPower = (int) statsHolder.ValueOf(StatId.ActionPower).Current;
            var summaryDamage = weaponDamage + actionPower;

            caster
                .GetComponent<IDamageDealer>()
                .DealDamage(new UnitAttackEvent {
                    target = targetNetIdentity,
                    damage = summaryDamage,
                    damageType = DamageType.Physical
                });
        }

    }

}