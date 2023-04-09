using System;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.SpellMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class SpellMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, GetAvailableSpellsRequestMessage> ServerGetAvailableSpellsReceived;
        public static event Action<GetAvailableSpellsResponseMessage> ClientGetAvailableSpellsReceived;
        public static event Action<FireballSpellVfxResponseMessage> ClientFireballVfxReceived;
        public static event Action<TransferManaSpellVfxResponseMessage> ClientTransferManaVfxReceived;
        public static event Action<HealSpellVfxResponseMessage> ClientHealVfxReceived;
        public static event Action<DarkNovaSpellVfxResponseMessage> ClientDarkNovaVfxReceived;
        public static event Action<CircleOfLightSpellVfxResponseMessage> ClientCircleOfLightVfxReceived;

        public void OnStartServer() {
            NetworkServer.RegisterHandler<GetAvailableSpellsRequestMessage>(ServerHandleGetAvailableSpells);
        }

        public void OnStopServer() {
            NetworkServer.UnregisterHandler<GetAvailableSpellsRequestMessage>();
        }

        public void OnStartClient() {
            NetworkClient.RegisterHandler<GetAvailableSpellsResponseMessage>(ClientHandleGetAvailableSpells);
            NetworkClient.RegisterHandler<FireballSpellVfxResponseMessage>(ClientHandleFireballSpellVfx);
            NetworkClient.RegisterHandler<TransferManaSpellVfxResponseMessage>(ClientHandleTransferManaSpellVfx);
            NetworkClient.RegisterHandler<HealSpellVfxResponseMessage>(ClientHandleHealSpellVfx);
            NetworkClient.RegisterHandler<DarkNovaSpellVfxResponseMessage>(ClientHandleDarkNovaSpellVfx);
            NetworkClient.RegisterHandler<CircleOfLightSpellVfxResponseMessage>(ClientHandleCircleOfLightSpellVfx);
        }

        public void OnStopClient() {
            NetworkClient.UnregisterHandler<GetAvailableSpellsResponseMessage>();
            NetworkClient.UnregisterHandler<FireballSpellVfxResponseMessage>();
            NetworkClient.UnregisterHandler<TransferManaSpellVfxResponseMessage>();
            NetworkClient.UnregisterHandler<HealSpellVfxResponseMessage>();
            NetworkClient.UnregisterHandler<DarkNovaSpellVfxResponseMessage>();
            NetworkClient.UnregisterHandler<CircleOfLightSpellVfxResponseMessage>();
        }

        [Server]
        private static void ServerHandleGetAvailableSpells(NetworkConnectionToClient conn,
                                                           GetAvailableSpellsRequestMessage message) =>
            ServerGetAvailableSpellsReceived?.Invoke(conn, message);

        [Client]
        private static void ClientHandleGetAvailableSpells(GetAvailableSpellsResponseMessage message) =>
            ClientGetAvailableSpellsReceived?.Invoke(message);

        [Client]
        private static void ClientHandleFireballSpellVfx(FireballSpellVfxResponseMessage message) =>
            ClientFireballVfxReceived?.Invoke(message);

        [Client]
        private static void ClientHandleTransferManaSpellVfx(TransferManaSpellVfxResponseMessage message) =>
            ClientTransferManaVfxReceived?.Invoke(message);

        private static void ClientHandleHealSpellVfx(HealSpellVfxResponseMessage message) =>
            ClientHealVfxReceived?.Invoke(message);

        private static void ClientHandleDarkNovaSpellVfx(DarkNovaSpellVfxResponseMessage message) =>
            ClientDarkNovaVfxReceived?.Invoke(message);

        private static void ClientHandleCircleOfLightSpellVfx(CircleOfLightSpellVfxResponseMessage message) =>
            ClientCircleOfLightVfxReceived?.Invoke(message);


    }

}