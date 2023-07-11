using Mirror;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public interface INetworkMessagesProxy {
        [Server]
        void OnStartServer();
        [Server]
        void OnStopServer();
        [Client]
        void OnStartClient();
        [Client]
        void OnStopClient();
    }

}