using System.Collections;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.TimedEffect {

    [CreateAssetMenu(fileName = nameof(FireballTimedEffect),
                     menuName = "DL/" + nameof(SpellTimedEffect) + "/" + nameof(FireballTimedEffect))]
    public class FireballTimedEffect : SpellTimedEffect {

        [SerializeField]
        private int spellDamage;
        [SerializeField]
        private int fireballProjectileSpeed;

        public override IEnumerator Process(GameObject caster) {
            var targetNetIdentity = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = targetNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var fireballPathLength = Vector3.Distance(castPos, targetPos);

            var fireballFlyDuration = fireballPathLength / fireballProjectileSpeed;
            // var fireballFlyDuration = 1.0f / fireballProjectileSpeed;

            var actionPower = Mathf.FloorToInt(caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current);
            var damageTimeOffset = 0.2f;

            NetworkServer.SendToReady(new SpellMessages.FireballSpellVfxResponseMessage {
                castPos = castPos,
                targetPos = targetPos,
                fireballFlyDuration = fireballFlyDuration
            });

            yield return new WaitForSeconds(fireballFlyDuration - damageTimeOffset);

            caster
                .GetComponent<IDamageDealer>()
                .DealDamage(new UnitAttackEvent {
                    target = targetNetIdentity,
                    damage = spellDamage + actionPower,
                    damageType = DamageType.Magic
                });
        }

        public override bool CanProcess(GameObject caster) {
            return caster.GetComponent<ITargetNetIdHolder>().HasTarget();
        }

        public override string Description(GameObject caster, ISpell spell) {
            var actionPower = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current;
            var resultDmg = spellDamage + actionPower;

            return "Launches fireball towards enemy.\n" +
                   $"Damage:\t{RichTextFormatter.Bold(resultDmg.ToString())}\n" +
                   $"Max range:\t{spell.CastRange}\n" +
                   $"Cooldown:\t{spell.Cooldown(caster):0.0} seconds";
        }

    }

}