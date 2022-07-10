using System;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class PlayerStatsHolder : StatsHolder {

        [SyncVar(hook = nameof(ClientSyncHealth))]
        [DarklandStat(StatId.Health, nameof(ServerSetHealth))]
        private StatValue _health;
        
        [SyncVar]
        [DarklandStat(StatId.MaxHealth, nameof(ServerSetMaxHealth))]
        private StatValue _maxHealth;
        
        [DarklandStat(StatId.HealthRegain, nameof(ServerSetHealthRegain))]
        private StatValue _healthRegain;

        [DarklandStat(StatId.MovementSpeed, nameof(ServerSetMovementSpeed))]
        private StatValue _movementSpeed;

        //todo nowy interface z tego
        public event Action<StatId, StatValue> ClientChanged;

        [Server]
        private void ServerSetHealth(StatValue val) => _health = val;
        [Server]
        private void ServerSetMaxHealth(StatValue val) => _maxHealth = val;
        [Server]
        private void ServerSetHealthRegain(StatValue val) => _healthRegain = val;
        [Server]
        private void ServerSetMovementSpeed(StatValue val) => _movementSpeed = val;

        [Client]
        private void ClientSyncHealth(StatValue _, StatValue val) {
            Debug.Log($"ClientSyncHealth {Time.time} {val.Current}", this);
            ClientChanged?.Invoke(StatId.Health, val);
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
     */

}