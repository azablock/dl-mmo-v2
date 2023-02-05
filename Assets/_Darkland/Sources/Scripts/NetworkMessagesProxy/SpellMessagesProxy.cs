using System;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.SpellMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class SpellMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, GetAvailableSpellsRequestMessage> ServerGetAvailableSpellsReceived;
        public static event Action<GetAvailableSpellsResponseMessage> ClientGetAvailableSpellsReceived;
        public static event Action<FireballSpellVfxResponseMessage> ClientFireballVfxReceived;

        public void OnStartServer() {
            NetworkServer.RegisterHandler<GetAvailableSpellsRequestMessage>(ServerHandleGetAvailableSpells);
        }

        public void OnStopServer() {
            NetworkServer.UnregisterHandler<GetAvailableSpellsRequestMessage>();
        }

        public void OnStartClient() {
            NetworkClient.RegisterHandler<GetAvailableSpellsResponseMessage>(ClientHandleGetAvailableSpells);
            NetworkClient.RegisterHandler<FireballSpellVfxResponseMessage>(ClientHandleFireballSpellVfx);
        }

        public void OnStopClient() {
            NetworkClient.UnregisterHandler<GetAvailableSpellsResponseMessage>();
            NetworkClient.UnregisterHandler<FireballSpellVfxResponseMessage>();
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

    }

}