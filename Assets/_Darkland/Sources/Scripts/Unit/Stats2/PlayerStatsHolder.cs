using System;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class PlayerStatsHolder : StatsHolder {

        [SyncVar(hook = nameof(ClientSyncHealth))]
        [DarklandStat(StatId.Health)]
        private StatValue _health;
        
        [SyncVar]
        [DarklandStat(StatId.MaxHealth)]
        private StatValue _maxHealth;
        
        [DarklandStat(StatId.HealthRegain)]
        private StatValue _hpRegain;

        //todo nowy interface z tego
        public event Action<StatId, StatValue> ClientChanged;

        //todo tmp - remove!!!
        public override void OnStartServer() {
            // Stat(StatId.HealthRegain).Set(Models.Unit.Stats2.StatValue.OfBasic(1));
            // Stat(StatId.MaxHealth).Set(Models.Unit.Stats2.StatValue.OfBasic(20));
            // Stat(StatId.Health).Set(Models.Unit.Stats2.StatValue.OfBasic(1));
        }

        //todo tmp - remove!!!
        public override void OnStartLocalPlayer() {
            CmdInit();
        }

        //todo tmp - remove!!!
        [Command]
        private void CmdInit() {
            Stat(StatId.HealthRegain).Set(Models.Unit.Stats2.StatValue.OfBasic(1));
            Stat(StatId.MaxHealth).Set(Models.Unit.Stats2.StatValue.OfBasic(20));
            Stat(StatId.Health).Set(Models.Unit.Stats2.StatValue.OfBasic(1));
        }

        //todo tmp - remove!!!
        private void Update() {
            if (Input.GetKeyDown(KeyCode.F1)) {
                CmdChangeStat(StatId.MaxHealth, 1);
            }

            if (Input.GetKeyDown(KeyCode.F2)) {
                CmdChangeStat(StatId.MaxHealth, -1);
            }
            
            if (Input.GetKeyDown(KeyCode.F3)) {
                CmdChangeStat(StatId.Health, 5);
            }

            if (Input.GetKeyDown(KeyCode.F4)) {
                CmdChangeStat(StatId.Health, -5);
            }
        }

        //todo tmp - remove!!!
        [Command]
        private void CmdChangeStat(StatId statId, int delta) {
            Stat(statId).Add(Models.Unit.Stats2.StatValue.OfBasic(delta));
        }

        [Client]
        private void ClientSyncHealth(StatValue _, StatValue val) {
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