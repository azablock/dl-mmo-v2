using System.Collections;
using System.Collections.Generic;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Spell;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.TimedEffect {

    [CreateAssetMenu(fileName = nameof(CircleOfLightTimedEffect),
                     menuName = "DL/" + nameof(SpellTimedEffect) + "/" + nameof(CircleOfLightTimedEffect))]
    public class CircleOfLightTimedEffect : SpellTimedEffect {

        [SerializeField]
        private int healthRegainBonus;
        [SerializeField]
        private int circleOfLightDuration;
        [SerializeField]
        private int circleOfLightRadius;
        [SerializeField]
        private GameObject circleOfLightPrefab;

        public override IEnumerator Process(GameObject caster) {
            var instance = Instantiate(circleOfLightPrefab, caster.transform.position, Quaternion.identity);
            var triggeredIdentities = new List<NetworkIdentity>();
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;
            
            instance.GetComponent<CircleOfLightSpellBodyBehaviour>()
                .ServerInit(circleOfLightRadius,
                            identity => ServerAddHealthRegainBuff(identity, triggeredIdentities),
                            identity => ServerSubtractHealthRegainBuff(identity, triggeredIdentities));

            NetworkServer.SendToReady(new SpellMessages.CircleOfLightSpellVfxResponseMessage {
                castPos = castPos,
                radius = circleOfLightRadius,
                duration = circleOfLightDuration
            });
            
            yield return new WaitForSeconds(circleOfLightDuration);
            Destroy(instance);
            
            triggeredIdentities.ForEach(it => {
                it.GetComponent<IStatsHolder>().Subtract(StatId.HealthRegain, StatVal.OfBonus(healthRegainBonus));
            });
        }

        public override bool CanProcess(GameObject caster) => true;

        public override string Description(GameObject caster, ISpell spell) {
            return $"Creates zone of radius {circleOfLightRadius} that grants {healthRegainBonus} bonus to Health Regain.\n" +
                   $"Cooldown:\t{spell.Cooldown(caster):0.0} seconds";
        }


        [Server]
        private void ServerAddHealthRegainBuff(NetworkIdentity identity, List<NetworkIdentity> triggeredIdentities) {
            if (triggeredIdentities.Contains(identity)) return;
            
            identity.GetComponent<IStatsHolder>().Add(StatId.HealthRegain, StatVal.OfBonus(healthRegainBonus));
            
            triggeredIdentities.Add(identity);
        }

        [Server]
        private void ServerSubtractHealthRegainBuff(NetworkIdentity identity, List<NetworkIdentity> triggeredIdentities) {
            if (!triggeredIdentities.Contains(identity)) return;

            identity.GetComponent<IStatsHolder>().Subtract(StatId.HealthRegain, StatVal.OfBonus(healthRegainBonus));

            triggeredIdentities.Remove(identity);
        }

    }

}