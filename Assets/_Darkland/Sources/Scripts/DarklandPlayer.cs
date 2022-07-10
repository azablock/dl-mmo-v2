using System;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandPlayer : NetworkBehaviour {

        public event Action ClientStarted;
        public static event Action LocalPlayerStarted;
        public static event Action LocalPlayerStopped;
        public static DarklandPlayer localPlayer;

        public override void OnStartLocalPlayer() {
            localPlayer = this;
            LocalPlayerStarted?.Invoke();
        }

        public override void OnStopLocalPlayer() {
            LocalPlayerStopped?.Invoke();
        }

        public override void OnStartClient() {
            ClientStarted?.Invoke();
        }
    }

}