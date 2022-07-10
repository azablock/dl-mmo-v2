using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.NetworkMessagesProxies;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandlers {

    public class PlayerInputMessagesHandler : MonoBehaviour {

        private void Awake() {
            PlayerInputMessagesProxy.ServerReceived += ServerProcess;
        }

        private void OnDestroy() {
            PlayerInputMessagesProxy.ServerReceived -= ServerProcess;
        }

        [Server]
        private static void ServerProcess(NetworkConnectionToClient conn, PlayerInputMessages.MoveRequestMessage message) {
            var movementBehaviour = conn.identity.GetComponent<MovementBehaviour>();
            var newPos = conn.identity.GetComponent<IDiscretePosition>().Pos + message.moveDelta;
            
            movementBehaviour.ServerSetDiscretePosition(newPos);
        }
    }

}