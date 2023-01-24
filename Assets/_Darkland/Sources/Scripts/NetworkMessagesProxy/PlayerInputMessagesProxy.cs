using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {
    
    public class PlayerInputMessagesProxy : MonoBehaviour, INetworkMessagesProxy {
        
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.MoveRequestMessage> ServerMove;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.ChangeFloorRequestMessage> ServerChangeFloor;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.NpcClickRequestMessage> ServerNpcClick;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.GetHealthStatsRequestMessage> ServerGetHealthStats;
        public static event Action<NetworkConnectionToClient, PlayerInputMessages.CastSpellRequestMessage> ServerCastSpell;

        public static event Action<PlayerInputMessages.GetHealthStatsResponseMessage> ClientGetHealthStats;

        [Server]
        public void OnStartServer() {
            NetworkServer.RegisterHandler<PlayerInputMessages.MoveRequestMessage>(ServerHandleMove);
            NetworkServer.RegisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>(ServerHandleChangeFloor);
            NetworkServer.RegisterHandler<PlayerInputMessages.NpcClickRequestMessage>(ServerHandleNpcClick);
            NetworkServer.RegisterHandler<PlayerInputMessages.GetHealthStatsRequestMessage>(ServerHandleGetHealthStats);
            NetworkServer.RegisterHandler<PlayerInputMessages.CastSpellRequestMessage>(ServerHandleCastSpell);
        }

        [Server]
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<PlayerInputMessages.MoveRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.ChangeFloorRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.NpcClickRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.GetHealthStatsRequestMessage>();
            NetworkServer.UnregisterHandler<PlayerInputMessages.CastSpellRequestMessage>();
        }

        [Client]
        public void OnStartClient() {
            NetworkClient.RegisterHandler<PlayerInputMessages.GetHealthStatsResponseMessage>(ClientHandleGetHealthStats);
        }

        [Client]
        public void OnStopClient() {
            NetworkClient.UnregisterHandler<PlayerInputMessages.GetHealthStatsResponseMessage>();
        }

        [Server]
        private static void ServerHandleMove(NetworkConnectionToClient conn,
                                             PlayerInputMessages.MoveRequestMessage message) =>
            ServerMove?.Invoke(conn, message);

        [Server]
        private static void ServerHandleChangeFloor(NetworkConnectionToClient conn,
                                                    PlayerInputMessages.ChangeFloorRequestMessage message) =>
            ServerChangeFloor?.Invoke(conn, message);

        [Server]
        private static void ServerHandleNpcClick(NetworkConnectionToClient conn,
                                                 PlayerInputMessages.NpcClickRequestMessage message) =>
            ServerNpcClick?.Invoke(conn, message);

        [Server]
        private static void ServerHandleGetHealthStats(NetworkConnectionToClient conn,
                                                       PlayerInputMessages.GetHealthStatsRequestMessage message) =>
            ServerGetHealthStats?.Invoke(conn, message);

        [Server]
        private static void ServerHandleCastSpell(NetworkConnectionToClient conn,
                                                  PlayerInputMessages.CastSpellRequestMessage message) =>
            ServerCastSpell?.Invoke(conn, message);

        [Client]
        private static void ClientHandleGetHealthStats(PlayerInputMessages.GetHealthStatsResponseMessage message) =>
            ClientGetHealthStats?.Invoke(message);

    }
}