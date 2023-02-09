using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Spell;
using _Darkland.Sources.Scripts.Unit.Combat;
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

            Instantiate(darkNovaPrefab, caster.transform.position, Quaternion.identity)
                .GetComponent<DarkNovaSpellBodyBehaviour>()
                .ServerInit(radius, mobIdentity => {
                    damageDealer.DealDamage(new UnitAttackEvent {
                        damage = Mathf.FloorToInt(novaDamage + actionPower),
                        target = mobIdentity,
                        damageType = DamageType.Magic
                    });
                });
            
            
            //todo send to client vfx
        }

    }

}