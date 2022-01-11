using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public sealed class DarklandPlayerMessagesProxy : NetworkBehaviour {

        public static Action<NetworkConnection, DarklandPlayerMessages.ActionRequestMessage> serverReceived;
        public static Action<DarklandPlayerMessages.ActionResponseMessage> clientReceived;
        
        public override void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandPlayerMessages.ActionRequestMessage>(ServerHandle);
        }

        public override void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandPlayerMessages.ActionRequestMessage>();
        }

        public override void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandPlayerMessages.ActionResponseMessage>(ClientHandle);
        }

        public override void OnStopClient() {
            NetworkClient.UnregisterHandler<DarklandPlayerMessages.ActionResponseMessage>();
        }

        [Server]
        private static void ServerHandle(NetworkConnection conn, DarklandPlayerMessages.ActionRequestMessage msg) {
            Debug.Log(
                $"DarklandPlayerMessagesProxy Server: ActionRequestMessage received from [netId={msg.darklandPlayerNetId}] at {NetworkTime.time}"
            );
            serverReceived?.Invoke(conn, msg);
        }

        [Client]
        private static void ClientHandle(DarklandPlayerMessages.ActionResponseMessage msg) {
            Debug.Log(
                $"DarklandPlayerMessagesProxy Client: ActionResponseMessage received from [netId={msg.darklandPlayerNetId}] at {NetworkTime.time}"
            );
            clientReceived?.Invoke(msg);
        }
    }

}