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
            var (health, maxHealth, mana, maxMana) = 
                statsHolder.Values(Tuple.Create(StatId.Health, StatId.MaxHealth, StatId.Mana, StatId.MaxMana));
            var unitName = statsHolderNetIdentity.GetComponent<UnitNameBehaviour>().unitName;

            conn.Send(new GetHealthStatsResponseMessage {
                health = health.Basic,
                maxHealth = maxHealth.Current,
                mana = mana.Basic,
                maxMana = maxMana.Current,
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

            var eqHolder = conn.identity.GetComponent<IEqHolder>();
            if (eqHolder.ServerBackpackFull()) return;
            
            var pickupPos = conn.identity.GetComponent<IDiscretePosition>().Pos;
            var itemPos = onGroundItem.Pos;
            var zEqual = itemPos.z == pickupPos.z;
            var inDistance = Vector3.Distance(pickupPos, itemPos) <= 2; //todo magic number

            if (zEqual && inDistance) {
                conn.identity.GetComponent<IEqHolder>().PickupFromGround(onGroundItem);
            }
        }

        [Server]
        private static void ServerProcessDropItem(NetworkConnectionToClient conn,
                                                  DropItemRequestMessage message) {
            var dropPos = conn.identity.GetComponent<IDiscretePosition>().Pos;
            
            //todo optimize!!!
            var itemsWithDropPos = NetworkServer.spawned.Values
                .Select(it => it.GetComponent<IOnGroundEqItem>())
                .Where(it => it != null)
                .Count(it => it.Pos.Equals(dropPos));
            
            //moge wyrzucic item - jesli tylko jeden przedmiot lezy w tym samym miejscu
            if (itemsWithDropPos > 1) return;

            conn.identity.GetComponent<IEqHolder>().DropOnGround(message.backpackSlot);
        }

        [Server]
        private static void ServerProcessUseItem(NetworkConnectionToClient conn,
                                                 UseItemRequestMessage message) {
            var eqHolder = conn.identity.GetComponent<IEqHolder>();
            var backpackSlot = message.backpackSlot;
            var item = eqHolder.ServerBackpackItem(message.backpackSlot);

            switch (item?.ItemType) {
                case EqItemType.Consumable:
                    eqHolder.UseConsumable(backpackSlot);
                    break;
                case EqItemType.Wearable:
                    eqHolder.EquipWearableFromBackpack(backpackSlot);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        [Server]
        private static void ServerProcessUnequipWearable(NetworkConnectionToClient conn,
                                                                  UnequipWearableRequestMessage message) {
            conn.identity.GetComponent<IEqHolder>().UnequipWearableToBackpack(message.wearableSlot);
        }

    }

}