using _Darkland.Sources.Models;
using _Darkland.Sources.Scripts;
using Mirror;
using Sources.Models;

namespace Sources.Scripts {

    public class SentActionRequestsManager : NetworkBehaviour {

        private ISentActionRequestsHandler _sentActionRequestsHandler;

        private void Awake() {
            _sentActionRequestsHandler = new BasicSentActionRequestsHandler();
        }

        public override void OnStartServer() {
            DarklandPlayerMessagesProxy.serverReceived += _sentActionRequestsHandler.ServerHandle;
        }

        public override void OnStopServer() {
            DarklandPlayerMessagesProxy.serverReceived -= _sentActionRequestsHandler.ServerHandle;
        }
    }

}