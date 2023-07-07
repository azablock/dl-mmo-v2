using System;
using Mirror;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandUnit : NetworkBehaviour {

        public event Action ServerStarted;
        public event Action ServerStopped;
        public event Action ClientStarted;

        public override void OnStartServer() {
            ServerStarted?.Invoke();
        }

        public override void OnStopServer() {
            ServerStopped?.Invoke();
        }

        public override void OnStartClient() {
            ClientStarted?.Invoke();
        }

    }

}