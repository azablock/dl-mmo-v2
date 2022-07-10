using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxies {

    public class PlayerInputMessagesProxy : MonoBehaviour {

        public static event Action<NetworkConnectionToClient, PlayerInputMessages.MoveRequestMessage> ServerReceived;

        private void Awake() {
            NetworkServer.RegisterHandler<PlayerInputMessages.MoveRequestMessage>(ServerHandle);
        }

        private void OnDestroy() {
            NetworkServer.UnregisterHandler<PlayerInputMessages.MoveRequestMessage>();
        }

        [Server]
        private static void ServerHandle(NetworkConnectionToClient conn,
                                         PlayerInputMessages.MoveRequestMessage message) =>
            ServerReceived?.Invoke(conn, message);
    }

}