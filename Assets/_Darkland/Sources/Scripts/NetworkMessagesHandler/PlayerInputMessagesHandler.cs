using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Ai;
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

            //todo TEST TEST TEST ----------------------------------------------------------------------
            if (message.movementVector.magnitude == 0) return;

            var targetNetIdentity = conn.identity.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            if (targetNetIdentity != null && targetNetIdentity.GetComponent<DarklandMob>() != null) {
                var healthStat = targetNetIdentity.GetComponent<IStatsHolder>().Stat(StatId.Health);
                var newHealthValue = healthStat.Get() - 1;
                healthStat.Set(newHealthValue);

                if (newHealthValue == 0) {
                    var heroXpHolder = conn.identity.GetComponent<XpHolderBehaviour>();
                    var heroName = heroXpHolder.GetComponent<UnitNameBehaviour>().unitName;
                    var xpGain = targetNetIdentity.GetComponent<XpGiverBehaviour>().xp;
                    heroXpHolder.ServerGain(xpGain);

                    var xpChatMessage = ChatMessagesFormatter.FormatServerLog($"Hero {heroName} has {heroXpHolder.xp} xp!");

                    NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage() {
                        message = xpChatMessage
                    });
                }
            }
            //todo TEST TEST TEST ----------------------------------------------------------------------
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
    }

}