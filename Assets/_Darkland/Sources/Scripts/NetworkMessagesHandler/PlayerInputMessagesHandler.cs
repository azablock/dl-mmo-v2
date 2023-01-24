using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.World;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Interaction;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Spell;
using _Darkland.Sources.Scripts.Unit;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class PlayerInputMessagesHandler : MonoBehaviour {

        private void Awake() {
            PlayerInputMessagesProxy.ServerMove += ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloor += ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClick += ServerProcessNpcClick;
            PlayerInputMessagesProxy.ServerGetHealthStats += ServerProcessGetHealthStats;
            PlayerInputMessagesProxy.ServerCastSpell += ServerProcessCastSpell;
        }
        

        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerMove -= ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloor -= ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClick -= ServerProcessNpcClick;
            PlayerInputMessagesProxy.ServerGetHealthStats -= ServerProcessGetHealthStats;
            PlayerInputMessagesProxy.ServerCastSpell -= ServerProcessCastSpell;
        }

        [Server]
        private static void ServerProcessMove(NetworkConnectionToClient conn,
                                              PlayerInputMessages.MoveRequestMessage message) {
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);
            
            //todo test 2 $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
            if (message.movementVector.x != 0) {
                conn.identity.GetComponent<IStatsHolder>()
                    .Stat(StatId.Might)
                    .Add(message.movementVector.x);
            }
            
            if (message.movementVector.y != 0) {
                conn.identity.GetComponent<IStatsHolder>()
                    .Stat(StatId.Constitution)
                    .Add(message.movementVector.y);
            }
            //todo test 2 $$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$

        }

        [Server]
        private static void ServerProcessChangeFloor(NetworkConnectionToClient conn,
                                                     PlayerInputMessages.ChangeFloorRequestMessage message) {
            var discretePosition = conn.identity.GetComponent<IDiscretePosition>();
            var currentPos = discretePosition.Pos;
            var possibleNextPos = currentPos + message.movementVector;
            var darklandWorld = DarklandWorldBehaviour._;

            var possibleNextPosIsEmptyField = darklandWorld.IsEmptyField(possibleNextPos);
            var canMoveUp = darklandWorld.IsLadderUp(currentPos) && message.movementVector.z == -1;
            var canMoveDown = darklandWorld.IsLadderDown(currentPos) && message.movementVector.z == 1;
            var canChangeFloor = possibleNextPosIsEmptyField && (canMoveUp || canMoveDown);

            if (canChangeFloor) discretePosition.Set(possibleNextPos, true);
        }

        [Server]
        private static void ServerProcessNpcClick(NetworkConnectionToClient conn,
                                                  PlayerInputMessages.NpcClickRequestMessage msg) {
            conn.identity.GetComponent<TargetNetIdHolderBehaviour>().Set(msg.npcNetId);
        }

        [Server]
        private static void ServerProcessGetHealthStats(NetworkConnectionToClient conn,
                                                        PlayerInputMessages.GetHealthStatsRequestMessage message) {
            var statsHolderNetId = message.statsHolderNetId;
            var statsHolderNetIdentity = NetworkServer.spawned[statsHolderNetId];
            var statsHolder = statsHolderNetIdentity.GetComponent<IStatsHolder>();
            var (health, maxHealth) = statsHolder.Values(StatId.Health, StatId.MaxHealth);
            var unitName = statsHolderNetIdentity.GetComponent<UnitNameBehaviour>().unitName;

            conn.Send(new PlayerInputMessages.GetHealthStatsResponseMessage {
                health = health,
                maxHealth = maxHealth,
                statsHolderNetId = statsHolderNetId,
                unitName = unitName
            });
        }
        
        [Server]
        private static void ServerProcessCastSpell(NetworkConnectionToClient conn,
                                                   PlayerInputMessages.CastSpellRequestMessage message) {
            conn.identity.GetComponent<ISpellCaster>().CastSpell(message.spellIdx);
        }

    }

}