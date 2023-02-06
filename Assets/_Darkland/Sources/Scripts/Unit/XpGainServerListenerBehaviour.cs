using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Unit.Combat;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class XpGainServerListenerBehaviour : NetworkBehaviour {

        private IDamageDealer _damageDealer;

        private void Awake() {
            _damageDealer = GetComponent<IDamageDealer>();
            _damageDealer.ServerDamageApplied += ServerOnDamageApplied;
        }

        private void OnDestroy() {
            _damageDealer.ServerDamageApplied -= ServerOnDamageApplied;
        }

        [Server]
        private void ServerOnDamageApplied(UnitAttackEvent evt) {
            if (evt.target.GetComponent<IStatsHolder>().ValueOf(StatId.Health).Basic > 0) return;

            var heroXpHolder = netIdentity.GetComponent<XpHolderBehaviour>();
            var heroName = netIdentity.GetComponent<UnitNameBehaviour>().unitName;
            var xpGain = evt.target.GetComponent<XpGiverBehaviour>().xp;
            heroXpHolder.ServerGain(xpGain);
            
            var message = $"Hero {heroName} has {heroXpHolder.xp} xp!";
            var xpChatMessage = RichTextFormatter.FormatServerLog(message);
            
            NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage {
                message = xpChatMessage
            });
        }

    }

}