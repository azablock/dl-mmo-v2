using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxies;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandlers {

    public class PlayerInputMessagesHandler : MonoBehaviour {

        private void Awake() {
            PlayerInputMessagesProxy.ServerMoveReceived += ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloorReceived += ServerProcessChangeFloor;
        }

        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerMoveReceived -= ServerProcessMove;
            PlayerInputMessagesProxy.ServerChangeFloorReceived -= ServerProcessChangeFloor;
        }

        [Server]
        private static void ServerProcessMove(NetworkConnectionToClient conn, PlayerInputMessages.MoveRequestMessage message) {
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);
        }

        [Server]
        private static void ServerProcessChangeFloor(NetworkConnectionToClient conn,
                                                     PlayerInputMessages.ChangeFloorRequestMessage message) {
            var discretePosition = conn.identity.GetComponent<IDiscretePosition>();
            discretePosition.SetClientImmediate(discretePosition.Pos + message.movementVector);
        }
    }

}