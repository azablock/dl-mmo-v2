using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class PlayerStatsHolder : StatsHolder {

        [SyncVar(hook = nameof(ClientSyncHealth))]
        [DarklandStat(StatId.Health, nameof(ServerSetHealth))]
        [SerializeField]
        private float health;

        [SyncVar(hook = nameof(ClientSyncMaxHealth))]
        [DarklandStat(StatId.MaxHealth, nameof(ServerSetMaxHealth))]
        [SerializeField]
        private float maxHealth;

        [DarklandStat(StatId.HealthRegain, nameof(ServerSetHealthRegain))]
        [SerializeField]
        private float healthRegain;

        [SyncVar(hook = nameof(ClientSyncMovementSpeed))]
        [DarklandStat(StatId.MovementSpeed, nameof(ServerSetMovementSpeed))]
        [SerializeField]
        private float movementSpeed;

        public override void OnStartServer() {
            ServerSetMovementSpeed(1);
        }

        [Server]
        private void ServerSetHealth(float val) => health = val;

        [Server]
        private void ServerSetMaxHealth(float val) => maxHealth = val;

        [Server]
        private void ServerSetHealthRegain(float val) => healthRegain = val;

        [Server]
        private void ServerSetMovementSpeed(float val) => movementSpeed = val;

        [Client]
        private void ClientSyncHealth(float _, float val) {
            InvokeClientChanged(StatId.Health, val);
        }

        [Client]
        private void ClientSyncMaxHealth(float _, float val) {
            InvokeClientChanged(StatId.MaxHealth, val);
        }

        [Client]
        private void ClientSyncMovementSpeed(float _, float val) {
            InvokeClientChanged(StatId.MovementSpeed, val);
        }
    }

    /*
     
     Rat mob:
     hp,
     hp regain,
     traits,
     stats (movement speed, attack speed, cast speed)
     
     Mage mob:
     hp,
     hp regain,
     mana,
     mana regain,
     traits,
     stats (movement speed, attack speed, cast speed) 
     
     
     Door:
     hp
     
     Player:
     hp,
     hp regain,
     mana,
     mana regain,
     traits,  
     stats (movement speed, attack speed, cast speed)
     xp,
     
     */

}