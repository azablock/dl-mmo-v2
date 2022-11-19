using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class PlayerInputMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, PlayerInputMessages.MoveRequestMessage> ServerMoveReceived;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.ChangeFloorRequestMessage> ServerChangeFloorReceived;

        [Server]
        public void OnStartServer() {
            NetworkServer.RegisterHandler<PlayerInputMessages.MoveRequestMessage>(ServerHandleMove);
            NetworkServer.RegisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>(ServerHandleChangeFloor);
        }

        [Server]
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<PlayerInputMessages.MoveRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>();
        }

        [Client]
        public void OnStartClient() {}

        [Client]
        public void OnStopClient() {}

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