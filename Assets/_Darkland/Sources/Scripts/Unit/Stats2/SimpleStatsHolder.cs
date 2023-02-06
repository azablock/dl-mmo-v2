using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class SimpleStatsHolder : StatsHolder {

        [Header("Traits")]
        [SyncVar(hook = nameof(ClientSyncMight))]
        [DarklandStat(StatId.Might, nameof(ServerSetMight))]
        [SerializeField]
        private StatVal might;

        [SyncVar(hook = nameof(ClientSyncConstitution))]
        [DarklandStat(StatId.Constitution, nameof(ServerSetConstitution))]
        [SerializeField]
        private StatVal constitution;

        [SyncVar(hook = nameof(ClientSyncDexterity))]
        [DarklandStat(StatId.Dexterity, nameof(ServerSetDexterity))]
        [SerializeField]
        private StatVal dexterity;

        [SyncVar(hook = nameof(ClientSyncIntellect))]
        [DarklandStat(StatId.Intellect, nameof(ServerSetIntellect))]
        [SerializeField]
        private StatVal intellect;

        [SyncVar(hook = nameof(ClientSyncSoul))]
        [DarklandStat(StatId.Soul, nameof(ServerSetSoul))]
        [SerializeField]
        private StatVal soul;
        
        [Header("Primary Stats")]
        [SyncVar(hook = nameof(ClientSyncHealth))]
        [DarklandStat(StatId.Health, nameof(ServerSetHealth))]
        [SerializeField]
        private StatVal health;

        [SyncVar(hook = nameof(ClientSyncMana))]
        [DarklandStat(StatId.Mana, nameof(ServerSetMana))]
        [SerializeField]
        private StatVal mana;
        
        [Header("Secondary Stats")]
        [SyncVar(hook = nameof(ClientSyncMaxHealth))]
        [DarklandStat(StatId.MaxHealth, nameof(ServerSetMaxHealth))]
        [SerializeField]
        private StatVal maxHealth;

        [DarklandStat(StatId.HealthRegain, nameof(ServerSetHealthRegain))]
        [SerializeField]
        private StatVal healthRegain;

        [SyncVar(hook = nameof(ClientSyncMaxMana))]
        [DarklandStat(StatId.MaxMana, nameof(ServerSetMaxMana))]
        [SerializeField]
        private StatVal maxMana;

        [DarklandStat(StatId.ManaRegain, nameof(ServerSetManaRegain))]
        [SerializeField]
        private StatVal manaRegain;

        [DarklandStat(StatId.ActionPower, nameof(ServerSetActionPower))]
        [SerializeField]
        private StatVal actionPower;

        [DarklandStat(StatId.ActionSpeed, nameof(ServerSetActionSpeed))]
        [SerializeField]
        private StatVal actionSpeed;

        [DarklandStat(StatId.MagicResistance, nameof(ServerSetMagicResistance))]
        [SerializeField]
        private StatVal magicResistance;

        [DarklandStat(StatId.PhysicalResistance, nameof(ServerSetPhysicalResistance))]
        [SerializeField]
        private StatVal physicalResistance;
        
        [SyncVar(hook = nameof(ClientSyncMovementSpeed))]
        [DarklandStat(StatId.MovementSpeed, nameof(ServerSetMovementSpeed))]
        [SerializeField]
        private StatVal movementSpeed;

        [Server]
        private void ServerSetMight(StatVal val) => might = val;
        [Server]
        private void ServerSetConstitution(StatVal val) => constitution = val;
        [Server]
        private void ServerSetDexterity(StatVal val) => dexterity = val;
        [Server]
        private void ServerSetIntellect(StatVal val) => intellect = val;
        [Server]
        private void ServerSetSoul(StatVal val) => soul = val;
        [Server]
        private void ServerSetHealth(StatVal val) => health = val;
        [Server]
        private void ServerSetMaxHealth(StatVal val) => maxHealth = val;
        [Server]
        private void ServerSetHealthRegain(StatVal val) => healthRegain = val;
        [Server]
        private void ServerSetMana(StatVal val) => mana = val;
        [Server]
        private void ServerSetMaxMana(StatVal val) => maxMana = val;
        [Server]
        private void ServerSetManaRegain(StatVal val) => manaRegain = val;
        [Server]
        private void ServerSetActionPower(StatVal val) => actionPower = val;
        [Server]
        private void ServerSetActionSpeed(StatVal val) => actionSpeed = val;
        [Server]
        private void ServerSetMagicResistance(StatVal val) => magicResistance = val;
        [Server]
        private void ServerSetPhysicalResistance(StatVal val) => physicalResistance = val;
        [Server]
        private void ServerSetMovementSpeed(StatVal val) => movementSpeed = val;

        [Client]
        private void ClientSyncMight(StatVal _, StatVal val) => InvokeClientChanged(StatId.Might, val);
        [Client]
        private void ClientSyncConstitution(StatVal _, StatVal val) => InvokeClientChanged(StatId.Constitution, val);
        [Client]
        private void ClientSyncDexterity(StatVal _, StatVal val) => InvokeClientChanged(StatId.Dexterity, val);
        [Client]
        private void ClientSyncIntellect(StatVal _, StatVal val) => InvokeClientChanged(StatId.Intellect, val);
        [Client]
        private void ClientSyncSoul(StatVal _, StatVal val) => InvokeClientChanged(StatId.Soul, val);
        [Client]
        private void ClientSyncHealth(StatVal _, StatVal val) => InvokeClientChanged(StatId.Health, val);
        [Client]
        private void ClientSyncMaxHealth(StatVal _, StatVal val) => InvokeClientChanged(StatId.MaxHealth, val);
        [Client]
        private void ClientSyncMana(StatVal _, StatVal val) => InvokeClientChanged(StatId.Mana, val);
        [Client]
        private void ClientSyncMaxMana(StatVal _, StatVal val) => InvokeClientChanged(StatId.MaxMana, val);
        [Client]
        private void ClientSyncMovementSpeed(StatVal _, StatVal val) => InvokeClientChanged(StatId.MovementSpeed, val);
  
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