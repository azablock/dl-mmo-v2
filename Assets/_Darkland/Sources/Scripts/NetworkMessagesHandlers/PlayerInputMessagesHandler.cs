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
            conn.identity.GetComponent<MovementBehaviour>().ServerSetMovementVector(message.movementVector);
        }
    }

}