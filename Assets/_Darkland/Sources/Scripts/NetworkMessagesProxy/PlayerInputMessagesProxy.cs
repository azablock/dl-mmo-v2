using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class PlayerInputMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, PlayerInputMessages.MoveRequestMessage> ServerMoveReceived;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.ChangeFloorRequestMessage> ServerChangeFloorReceived;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.NpcClickRequestMessage> ServerNpcClickReceived;

        [Server]
        public void OnStartServer() {
            NetworkServer.RegisterHandler<PlayerInputMessages.MoveRequestMessage>(ServerHandleMove);
            NetworkServer.RegisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>(ServerHandleChangeFloor);
            NetworkServer.RegisterHandler<PlayerInputMessages.NpcClickRequestMessage>(ServerHandleNpcClick);
        }


        [Server]
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<PlayerInputMessages.MoveRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.NpcClickRequestMessage>();
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

        [Server]
        private static void ServerHandleNpcClick(NetworkConnectionToClient conn,
                                                 PlayerInputMessages.NpcClickRequestMessage message) =>
            ServerNpcClickReceived?.Invoke(conn, message);

    }

}