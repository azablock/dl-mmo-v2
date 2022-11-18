using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxies {

    public class PlayerInputMessagesProxy : MonoBehaviour {

        public static event Action<NetworkConnectionToClient, PlayerInputMessages.MoveRequestMessage> ServerMoveReceived;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.ChangeFloorRequestMessage> ServerChangeFloorReceived;

        private void Awake() {
            NetworkServer.RegisterHandler<PlayerInputMessages.MoveRequestMessage>(ServerHandleMove);
            NetworkServer.RegisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>(ServerHandleChangeFloor);
        }

        private void OnDestroy() {
            NetworkServer.UnregisterHandler<PlayerInputMessages.MoveRequestMessage>();
        }

        [Server]
        private static void ServerHandleMove(NetworkConnectionToClient conn, 
                                             PlayerInputMessages.MoveRequestMessage message) => 
            ServerMoveReceived?.Invoke(conn, message);

        [Server]
        private static void ServerHandleChangeFloor(NetworkConnectionToClient conn, 
                                                    PlayerInputMessages.ChangeFloorRequestMessage message) => 
            ServerChangeFloorReceived?.Invoke(conn, message);
    }

}