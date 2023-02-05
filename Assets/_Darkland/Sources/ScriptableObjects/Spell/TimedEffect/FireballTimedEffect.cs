using System.Collections;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.TimedEffect {

    [CreateAssetMenu(fileName = nameof(FireballTimedEffect),
                     menuName = "DL/"  + nameof(SpellTimedEffect) + "/" + nameof(FireballTimedEffect))]
    public class FireballTimedEffect : SpellTimedEffect {

        [SerializeField]
        private int spellDamage;
        [SerializeField]
        private int fireballProjectileSpeed;
        
        public override IEnumerator Process(GameObject caster) {
            var targetNetIdentity = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = targetNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var fireballFlyDuration = 1.0f / fireballProjectileSpeed;
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
                    damage = spellDamage,
                    damageType = DamageType.Magic
                });
        }

        public override bool CanProcess(GameObject caster) => caster.GetComponent<ITargetNetIdHolder>().HasTarget();

    }

}