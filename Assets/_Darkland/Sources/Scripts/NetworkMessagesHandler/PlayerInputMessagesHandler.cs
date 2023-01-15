using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Interaction;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
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
        }


        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerMove -= ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloor -= ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClick -= ServerProcessNpcClick;
            PlayerInputMessagesProxy.ServerGetHealthStats -= ServerProcessGetHealthStats;
        }

        [Server]
        private static void ServerProcessMove(NetworkConnectionToClient conn,
                                              PlayerInputMessages.MoveRequestMessage message) {
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);

            if (message.movementVector.magnitude == 0) return;

            //todo TEST TEST TEST
            var targetNetIdentity = conn.identity.GetComponent<ITargetNetIdHolder>().targetNetIdentity;
            if (targetNetIdentity != null) {
                var healthStat = targetNetIdentity.GetComponent<IStatsHolder>().Stat(StatId.Health);
                healthStat.Set(healthStat.Get() - 1);
            }
        }

        [Server]
        private static void ServerProcessChangeFloor(NetworkConnectionToClient conn,
                                                     PlayerInputMessages.ChangeFloorRequestMessage message) {
            var discretePosition = conn.identity.GetComponent<IDiscretePosition>();
            var possibleNextPosition = discretePosition.Pos + message.movementVector;

            if (WorldInteractionFilters.IsEmptyField(DarklandWorldBehaviour._, possibleNextPosition)) {
                discretePosition.Set(possibleNextPosition, true);
            }
        }

        [Server]
        private static void ServerProcessNpcClick(NetworkConnectionToClient conn,
                                                  PlayerInputMessages.NpcClickRequestMessage msg) {
            var message = ChatMessagesFormatter.FormatServerLog($"Npc netId[{msg.npcNetId}] clicked.");
            conn.Send(new ChatMessages.ServerLogResponseMessage { message = message });

            conn.identity.GetComponent<TargetNetIdHolderBehaviour>().ServerSet(msg.npcNetId);
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
    }

}