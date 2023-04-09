using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Spell;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(DarkNovaSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(DarkNovaSpellInstantEffect))]
    public class DarkNovaSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private GameObject darkNovaPrefab;
        [SerializeField]
        private float radius;
        [SerializeField]
        private int novaDamage;
        
        public override void Process(GameObject caster) {
            var damageDealer = caster.GetComponent<IDamageDealer>();
            var actionPower = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current;
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;

            Instantiate(darkNovaPrefab, castPos, Quaternion.identity)
                .GetComponent<DarkNovaSpellBodyBehaviour>()
                .ServerInit(radius, mobIdentity => {
                    damageDealer.DealDamage(new UnitAttackEvent {
                        damage = Mathf.FloorToInt(novaDamage + actionPower),
                        target = mobIdentity,
                        damageType = DamageType.Magic
                    });
                });
            
            NetworkServer.SendToReady(new SpellMessages.DarkNovaSpellVfxResponseMessage {
                castPos = castPos,
                radius = radius,
            });
        }

        public override string Description(GameObject caster) {
            var actionPower = Mathf.FloorToInt(caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current);

            return $"Creates dark energy zone of {radius} radius, " +
                   $"that deals {novaDamage + actionPower} damage to every enemy is range.";
        }

    }

}