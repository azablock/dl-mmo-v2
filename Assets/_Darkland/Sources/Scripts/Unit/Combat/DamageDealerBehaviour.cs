using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Ai;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Combat {

    public struct UnitAttackEvent {

        public NetworkIdentity target;
        public int damage;
        public DamageType damageType;

    }
    
    public class DamageDealerBehaviour : NetworkBehaviour {

        [Server]
        public void ServerDealDamage(UnitAttackEvent evt) {

            var healthStat = evt.target.GetComponent<IStatsHolder>().Stat(StatId.Health);
            var newHealthValue = healthStat.Get() - evt.damage;
            healthStat.Set(newHealthValue);

            //maybe better to sub to Death event
            // evt.targetStatsHolder.Subtract(StatId.Health, evt.damage); ??
            if (newHealthValue > 0) return;
            
            var heroXpHolder = netIdentity.GetComponent<XpHolderBehaviour>();
            var heroName = netIdentity.GetComponent<UnitNameBehaviour>().unitName;
            var xpGain = evt.target.GetComponent<XpGiverBehaviour>().xp;
            heroXpHolder.ServerGain(xpGain);

            var message = $"Hero {heroName} has {heroXpHolder.xp} xp!";
            var xpChatMessage = ChatMessagesFormatter.FormatServerLog(message);

            NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage {
                message = xpChatMessage
            });
        }

    }

}