using System;
using System.Linq;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.World;
using _Darkland.Sources.Scripts.Interaction;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.PlayerInputMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class PlayerInputMessagesHandler : MonoBehaviour {

        private void Awake() {
            PlayerInputMessagesProxy.ServerMove += ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloor += ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClick += ServerProcessNpcClick;
            PlayerInputMessagesProxy.ServerGetHealthStats += ServerProcessGetHealthStats;
            PlayerInputMessagesProxy.ServerCastSpell += ServerProcessCastSpell;
            PlayerInputMessagesProxy.ServerPickupItem += ServerProcessPickupItem;
            PlayerInputMessagesProxy.ServerDropItem += ServerProcessDropItem;
            PlayerInputMessagesProxy.ServerUseItem += ServerProcessUseItem;
            PlayerInputMessagesProxy.ServerUnequipWearable += ServerProcessUnequipWearable;
        }
        
        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerMove -= ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloor -= ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClick -= ServerProcessNpcClick;
            PlayerInputMessagesProxy.ServerGetHealthStats -= ServerProcessGetHealthStats;
            PlayerInputMessagesProxy.ServerCastSpell -= ServerProcessCastSpell;
            PlayerInputMessagesProxy.ServerPickupItem -= ServerProcessPickupItem;
            PlayerInputMessagesProxy.ServerDropItem -= ServerProcessDropItem;
            PlayerInputMessagesProxy.ServerUseItem -= ServerProcessUseItem;
            PlayerInputMessagesProxy.ServerUnequipWearable -= ServerProcessUnequipWearable;
        }

        [Server]
        private static void ServerProcessMove(NetworkConnectionToClient conn,
                                              MoveRequestMessage message) {
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);
        }

        [Server]
        private static void ServerProcessChangeFloor(NetworkConnectionToClient conn,
                                                     ChangeFloorRequestMessage message) {
            var discretePosition = conn.identity.GetComponent<IDiscretePosition>();
            var currentPos = discretePosition.Pos;
            var darklandWorld = DarklandWorldBehaviour._;
            var ladderTile = darklandWorld.TeleportTile(currentPos);

            if (ladderTile == null) return;

            var possibleNextPos = currentPos + ladderTile.posDelta;
            var canChangeFloor = darklandWorld.IsEmptyField(possibleNextPos);

            if (canChangeFloor) discretePosition.Set(possibleNextPos, true);
        }

        [Server]
        private static void ServerProcessNpcClick(NetworkConnectionToClient conn,
                                                  NpcClickRequestMessage msg) {
            conn.identity.GetComponent<TargetNetIdHolderBehaviour>().Set(msg.npcNetId);
        }

        [Server]
        private static void ServerProcessGetHealthStats(NetworkConnectionToClient conn,
                                                        GetHealthStatsRequestMessage message) {
            var statsHolderNetId = message.statsHolderNetId;
            var statsHolderNetIdentity = NetworkServer.spawned[statsHolderNetId];
            var statsHolder = statsHolderNetIdentity.GetComponent<IStatsHolder>();
            var (health, maxHealth) = statsHolder.Values(StatId.Health, StatId.MaxHealth);
            var unitName = statsHolderNetIdentity.GetComponent<UnitNameBehaviour>().unitName;

            conn.Send(new GetHealthStatsResponseMessage {
                health = health,
                maxHealth = maxHealth,
                statsHolderNetId = statsHolderNetId,
                unitName = unitName
            });
        }

        [Server]
        private static void ServerProcessCastSpell(NetworkConnectionToClient conn,
                                                   CastSpellRequestMessage message) {
            conn.identity.GetComponent<ISpellCaster>().CastSpell(message.spellIdx);
        }

        [Server]
        private static void ServerProcessPickupItem(NetworkConnectionToClient conn,
                                                    PickupItemRequestMessage message) {
            //todo optimize!!!
            var onGroundItem = NetworkServer.spawned.Values
                .Select(it => it.GetComponent<IOnGroundEqItem>())
                .Where(it => it != null)
                .FirstOrDefault(it => it.Pos.Equals(message.eqItemPos));
            
            if (onGroundItem == null) return;
            
            conn.identity.GetComponent<IEqHolder>().PickupFromGround(onGroundItem);
        }

        [Server]
        private static void ServerProcessDropItem(NetworkConnectionToClient conn,
                                                  DropItemRequestMessage message) {
            conn.identity.GetComponent<IEqHolder>().DropOnGround(message.backpackSlot);
        }

        [Server]
        private static void ServerProcessUseItem(NetworkConnectionToClient conn,
                                                 UseItemRequestMessage message) {
            var eqHolder = conn.identity.GetComponent<IEqHolder>();
            var backpackSlot = message.backpackSlot;
            var item = eqHolder.Backpack[backpackSlot];

            switch (item?.ItemType) {
                case EqItemType.Consumable:
                    eqHolder.UseConsumable(backpackSlot);
                    break;
                case EqItemType.Wearable:
                    eqHolder.EquipWearable(backpackSlot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        [Server]
        private static void ServerProcessUnequipWearable(NetworkConnectionToClient conn,
                                                                  UnequipWearableRequestMessage message) {
            conn.identity.GetComponent<IEqHolder>().UnequipWearable(message.wearableSlot);
        }

    }

}