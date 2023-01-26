using System.Collections;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.TimedEffect {

    [CreateAssetMenu(fileName = nameof(BurnSpellTimedEffect),
                     menuName = "DL/"  + nameof(SpellTimedEffect) + "/" + nameof(BurnSpellTimedEffect))]
    public class BurnSpellTimedEffect : SpellTimedEffect {

        public int burnRepeats;
        public float burnInterval;
        public int burnDamage;

        [Server]
        public override void PreProcess(GameObject caster) {
            Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"BurnSpellTimedEffect.Start()"));
        }

        [Server]
        public override void PostProcess(GameObject caster) {
            Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"BurnSpellTimedEffect.Stop()"));
        }

        [Server]
        public override IEnumerator Process(GameObject caster) {
            var remainingBurns = burnRepeats;

            while (remainingBurns >= 0) {
                var target = caster
                    .GetComponent<ITargetNetIdHolder>()
                    .TargetNetIdentity;
                
                caster
                    .GetComponent<IDamageDealer>()
                    .DealDamage(new UnitAttackEvent() {
                        damage = burnDamage,
                        target = target,
                        damageType = DamageType.Physical
                    });

                //todo send message "burn damage effect" on enemy
                NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage());

                yield return new WaitForSeconds(burnInterval);
                remainingBurns--;
            }
        }

        public override bool IsValid(GameObject caster) {
            return caster
                .GetComponent<ITargetNetIdHolder>()
                .TargetNetIdentity != null;
        }

    }

}