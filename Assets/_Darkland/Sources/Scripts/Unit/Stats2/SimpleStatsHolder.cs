using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class SimpleStatsHolder : StatsHolder {

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

        [SyncVar(hook = nameof(ClientSyncMight))]
        [DarklandStat(StatId.Might, nameof(ServerSetMight))]
        [SerializeField]
        private float might;

        [SyncVar(hook = nameof(ClientSyncConstitution))]
        [DarklandStat(StatId.Constitution, nameof(ServerSetConstitution))]
        [SerializeField]
        private float constitution;

        [SyncVar(hook = nameof(ClientSyncDexterity))]
        [DarklandStat(StatId.Dexterity, nameof(ServerSetDexterity))]
        [SerializeField]
        private float dexterity;

        [SyncVar(hook = nameof(ClientSyncIntellect))]
        [DarklandStat(StatId.Intellect, nameof(ServerSetIntellect))]
        [SerializeField]
        private float intellect;

        [SyncVar(hook = nameof(ClientSyncSoul))]
        [DarklandStat(StatId.Soul, nameof(ServerSetSoul))]
        [SerializeField]
        private float soul;

        [Server]
        private void ServerSetHealth(float val) => health = val;
        [Server]
        private void ServerSetMaxHealth(float val) => maxHealth = val;
        [Server]
        private void ServerSetHealthRegain(float val) => healthRegain = val;
        [Server]
        private void ServerSetMovementSpeed(float val) => movementSpeed = val;
        [Server]
        private void ServerSetMight(float val) => might = val;
        [Server]
        private void ServerSetConstitution(float val) => constitution = val;
        [Server]
        private void ServerSetDexterity(float val) => dexterity = val;
        [Server]
        private void ServerSetIntellect(float val) => intellect = val;
        [Server]
        private void ServerSetSoul(float val) => soul = val;
        [Client]
        private void ClientSyncHealth(float _, float val) => InvokeClientChanged(StatId.Health, val);
        [Client]
        private void ClientSyncMaxHealth(float _, float val) => InvokeClientChanged(StatId.MaxHealth, val);
        [Client]
        private void ClientSyncMovementSpeed(float _, float val) => InvokeClientChanged(StatId.MovementSpeed, val);
        [Client]
        private void ClientSyncMight(float _, float val) => InvokeClientChanged(StatId.Might, val);
        [Client]
        private void ClientSyncConstitution(float _, float val) => InvokeClientChanged(StatId.Constitution, val);
        [Client]
        private void ClientSyncDexterity(float _, float val) => InvokeClientChanged(StatId.Dexterity, val);
        [Client]
        private void ClientSyncIntellect(float _, float val) => InvokeClientChanged(StatId.Intellect, val);
        [Client]
        private void ClientSyncSoul(float _, float val) => InvokeClientChanged(StatId.Soul, val);

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