using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Unit.Combat;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(WeaponDmgSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(WeaponDmgSpellInstantEffect))]
    public class WeaponDmgSpellInstantEffect : SpellInstantEffect {

        public override void Process(GameObject caster) {
            //todo mock - get from equipment, somehow calculate by weapon stats and unit stats(traits?)
            var weaponDamage = Random.Range(3, 7);

            var targetNetIdentity = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;

            caster
                .GetComponent<IDamageDealer>()
                .DealDamage(new UnitAttackEvent {
                    target = targetNetIdentity,
                    damage = weaponDamage,
                    damageType = DamageType.Physical
                });
        }

        public override bool IsValid(GameObject caster) => caster.GetComponent<ITargetNetIdHolder>().HasTarget();

    }

}