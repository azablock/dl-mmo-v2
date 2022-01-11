using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts;
using Mirror;
using Sources.Models;
using Sources.Scripts;

namespace _Darkland.Sources.Models {

    public sealed class BasicSentActionRequestsHandler : ISentActionRequestsHandler {

        [Server]
        public void ServerHandle(NetworkConnection conn, DarklandPlayerMessages.ActionRequestMessage msg) {
            var sentActionRequestMessagesCountHolder = conn.identity.GetComponent<SentActionRequestMessagesCountHolder>();
            sentActionRequestMessagesCountHolder.ServerIncrement();

            NetworkServer.SendToAll(new DarklandPlayerMessages.ActionResponseMessage {
                    darklandPlayerNetId = msg.darklandPlayerNetId,
                    sentActionRequestMessagesCount = sentActionRequestMessagesCountHolder.count
                }
            );
        }
    }

}