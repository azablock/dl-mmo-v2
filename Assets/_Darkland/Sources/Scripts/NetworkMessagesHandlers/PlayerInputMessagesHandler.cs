using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Movement;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxies;
using _Darkland.Sources.Scripts.World;
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
            var possibleNextPosition = discretePosition.Pos + message.movementVector;

            if (MovementStaticObstacleFilter.ServerCanMove(WorldRootBehaviour2._, discretePosition.Pos, possibleNextPosition)) {
                discretePosition.SetClientImmediate(possibleNextPosition);
            }
        }
    }

}