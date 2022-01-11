using _Darkland.Sources.NetworkMessages;
using Mirror;

namespace Sources.Models {

    public interface ISentActionRequestsHandler {

        [Server]
        void ServerHandle(NetworkConnection conn, DarklandPlayerMessages.ActionRequestMessage msg);
    }

}