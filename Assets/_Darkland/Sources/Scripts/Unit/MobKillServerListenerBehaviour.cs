using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.ChatMessages;

namespace _Darkland.Sources.Scripts.Unit {

    public class MobKillServerListenerBehaviour : NetworkBehaviour {

        private XpHolderBehaviour _xpHolder;
        private IGoldHolder _goldHolder;
        private IDamageDealer _damageDealer;

        private void Awake() {
            _xpHolder = GetComponent<XpHolderBehaviour>();
            _goldHolder = GetComponent<IGoldHolder>();
            _damageDealer = GetComponent<IDamageDealer>();
            _damageDealer.ServerDamageApplied += ServerOnDamageApplied;
        }

        private void OnDestroy() {
            _damageDealer.ServerDamageApplied -= ServerOnDamageApplied;
        }

        [Server]
        private void ServerOnDamageApplied(DamageAppliedEvent evt) {
            Debug.LogError(RichTextFormatter.FormatServerLog($"targetHealthAfterDamage: {evt.targetHealthAfterDamage}"));
            
            if (evt.targetHealthAfterDamage > 0) return;

            var mobDef = evt.target.GetComponent<IMobDefHolder>().MobDef;
            var xpGain = mobDef.XpGain;
            _xpHolder.ServerGain(xpGain);

            var goldGain = Random.Range(mobDef.MinGoldGain, mobDef.MaxGoldGain + 1);
            _goldHolder.ServerAddGold(goldGain);

            var message = $"You gained {xpGain} xp and {goldGain} gold!";
            
            netIdentity
                .connectionToClient
                .Send(new ServerLogResponseMessage { message = RichTextFormatter.FormatServerLog(message) });
        }

    }

}