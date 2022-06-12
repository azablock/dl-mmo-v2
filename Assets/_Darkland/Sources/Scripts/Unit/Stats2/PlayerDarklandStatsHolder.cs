using System;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class PlayerDarklandStatsHolder : DarklandStatsHolder {

        [SyncVar]
        [DarklandStat(id = DarklandStatId.Health, getter = nameof(Health), setter = nameof(ServerSetHealth))]
        private FloatStat _health;
        [SyncVar]
        private FloatStat _maxHealth;
        
        private FloatStat _healthRegenRate;

        public event Action<DarklandStatId, FloatStat> ClientChanged;

        [Server]
        private void ServerSetHealth(FloatStat val) {
            _health = val;
        }

        [Server]
        private void ServerSetMaxHealth(FloatStat val) {
            _maxHealth = val;
        }

        [Server]
        public FloatStat Health() {
            return _health;
        }

        [Server]
        public FloatStat MaxHealth() {
            return _maxHealth;
        }

        [Server]
        public FloatStat HealthRegenRate() {
            return _healthRegenRate;
        }

        [Client]
        private void ClientSyncHealth(FloatStat _, FloatStat val) {
            ClientChanged?.Invoke(DarklandStatId.Health, val);
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