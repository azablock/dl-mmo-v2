using System;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxies {

    //todo to sie przyda?
    public abstract class NetworkMessageProxy<T> : MonoBehaviour where T : struct, NetworkMessage {
        
        public static event Action<NetworkConnectionToClient, T> ServerReceived;

        private void Awake() {
            NetworkServer.RegisterHandler<T>(ServerHandle);
        }

        private void OnDestroy() {
            NetworkServer.UnregisterHandler<T>();
        }

        [Server]
        private static void ServerHandle(NetworkConnectionToClient conn, T message) => ServerReceived?.Invoke(conn, message);
    }

}