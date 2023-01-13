using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Interaction;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class PlayerInputMessagesHandler : MonoBehaviour {

        private void Awake() {
            PlayerInputMessagesProxy.ServerMoveReceived += ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloorReceived += ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClickReceived += ServerProcessNpcClick;
        }
        
        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerMoveReceived -= ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloorReceived -= ServerProcessChangeFloor;
            PlayerInputMessagesProxy.ServerNpcClickReceived -= ServerProcessNpcClick;
        }

        [Server]
        private static void ServerProcessMove(NetworkConnectionToClient conn,
                                              PlayerInputMessages.MoveRequestMessage message) {
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);
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
                                                  PlayerInputMessages.NpcClickRequestMessage msg){
            var message = ChatMessagesFormatter.FormatServerLog($"Npc netId[{msg.npcNetId}] clicked.");
            conn.Send(new ChatMessages.ServerLogResponseMessage {message = message});
            
            conn.identity.GetComponent<TargetNetIdHolderBehaviour>().ServerSet(msg.npcNetId);
        }

    }

}