using System;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.PlayerInputMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class PlayerInputMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, MoveRequestMessage> ServerMove;
        public static event Action<NetworkConnectionToClient, ChangeFloorRequestMessage> ServerChangeFloor;
        public static event Action<NetworkConnectionToClient, NpcClickRequestMessage> ServerNpcClick;
        public static event Action<NetworkConnectionToClient, NpcClearRequestMessage> ServerNpcClear;
        public static event Action<NetworkConnectionToClient, GetHealthStatsRequestMessage> ServerGetHealthStats;
        public static event Action<NetworkConnectionToClient, CastSpellRequestMessage> ServerCastSpell;
        public static event Action<NetworkConnectionToClient, PickupItemRequestMessage> ServerPickupItem;
        public static event Action<NetworkConnectionToClient, DropItemRequestMessage> ServerDropItem;
        public static event Action<NetworkConnectionToClient, UseItemRequestMessage> ServerUseItem;
        public static event Action<NetworkConnectionToClient, UnequipWearableRequestMessage> ServerUnequipWearable;

        public static event Action<GetHealthStatsResponseMessage> ClientGetHealthStats;

        [Server]
        public void OnStartServer() {
            NetworkServer.RegisterHandler<MoveRequestMessage>(ServerHandleMove);
            NetworkServer.RegisterHandler<ChangeFloorRequestMessage>(ServerHandleChangeFloor);
            NetworkServer.RegisterHandler<NpcClickRequestMessage>(ServerHandleNpcClick);
            NetworkServer.RegisterHandler<NpcClearRequestMessage>(ServerHandleNpcClear);
            NetworkServer.RegisterHandler<GetHealthStatsRequestMessage>(ServerHandleGetHealthStats);
            NetworkServer.RegisterHandler<CastSpellRequestMessage>(ServerHandleCastSpell);
            NetworkServer.RegisterHandler<PickupItemRequestMessage>(ServerHandlePickupItem);
            NetworkServer.RegisterHandler<DropItemRequestMessage>(ServerHandleDropItem);
            NetworkServer.RegisterHandler<UseItemRequestMessage>(ServerHandleUseItem);
            NetworkServer.RegisterHandler<UnequipWearableRequestMessage>(ServerHandleUnequipWearable);
        }
        
        [Server]
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<MoveRequestMessage>();
            NetworkServer.UnregisterHandler<ChangeFloorRequestMessage>();
            NetworkServer.UnregisterHandler<NpcClickRequestMessage>();
            NetworkServer.UnregisterHandler<NpcClearRequestMessage>();
            NetworkServer.UnregisterHandler<GetHealthStatsRequestMessage>();
            NetworkServer.UnregisterHandler<CastSpellRequestMessage>();
            NetworkServer.UnregisterHandler<PickupItemRequestMessage>();
            NetworkServer.UnregisterHandler<DropItemRequestMessage>();
            NetworkServer.UnregisterHandler<UseItemRequestMessage>();
            NetworkServer.UnregisterHandler<UnequipWearableRequestMessage>();
        }

        [Client]
        public void OnStartClient() {
            NetworkClient.RegisterHandler<GetHealthStatsResponseMessage>(ClientHandleGetHealthStats);
        }

        [Client]
        public void OnStopClient() {
            NetworkClient.UnregisterHandler<GetHealthStatsResponseMessage>();
        }

        [Server]
        private static void ServerHandleMove(NetworkConnectionToClient conn,
                                             MoveRequestMessage message) =>
            ServerMove?.Invoke(conn, message);

        [Server]
        private static void ServerHandleChangeFloor(NetworkConnectionToClient conn,
                                                    ChangeFloorRequestMessage message) =>
            ServerChangeFloor?.Invoke(conn, message);

        [Server]
        private static void ServerHandleNpcClick(NetworkConnectionToClient conn,
                                                 NpcClickRequestMessage message) =>
            ServerNpcClick?.Invoke(conn, message);

        [Server]
        private static void ServerHandleNpcClear(NetworkConnectionToClient conn,
                                                 NpcClearRequestMessage message) =>
            ServerNpcClear?.Invoke(conn, message);


        [Server]
        private static void ServerHandleGetHealthStats(NetworkConnectionToClient conn,
                                                       GetHealthStatsRequestMessage message) =>
            ServerGetHealthStats?.Invoke(conn, message);

        [Server]
        private static void ServerHandleCastSpell(NetworkConnectionToClient conn,
                                                  CastSpellRequestMessage message) =>
            ServerCastSpell?.Invoke(conn, message);

        [Server]
        private static void ServerHandlePickupItem(NetworkConnectionToClient conn,
                                                   PickupItemRequestMessage message) =>
            ServerPickupItem?.Invoke(conn, message);

        [Server]
        private static void ServerHandleDropItem(NetworkConnectionToClient conn,
                                                 DropItemRequestMessage message) =>
            ServerDropItem?.Invoke(conn, message);

        [Server]
        private static void ServerHandleUseItem(NetworkConnectionToClient conn,
                                                UseItemRequestMessage message) =>
            ServerUseItem?.Invoke(conn, message);

        private static void ServerHandleUnequipWearable(NetworkConnectionToClient conn,
                                                        UnequipWearableRequestMessage message) =>
            ServerUnequipWearable?.Invoke(conn, message);

        [Client]
        private static void ClientHandleGetHealthStats(GetHealthStatsResponseMessage message) =>
            ClientGetHealthStats?.Invoke(message);

    }

}