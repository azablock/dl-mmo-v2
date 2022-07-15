using System;
using _Darkland.Sources.Models;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandPlayer : NetworkBehaviour {

        //todo tmp
        [field: SyncVar]
        public string characterName { get; private set; }

        public event Action ClientStarted;
        public static event Action LocalPlayerStarted;
        public static event Action LocalPlayerStopped;
        public static DarklandPlayer localPlayer;

        public override void OnStartServer() {
            characterName = $"{CharacterNames.RandomName()} {CharacterNames.RandomName()}";
        }

        public override void OnStartClient() {
            ClientStarted?.Invoke();
        }

        public override void OnStartLocalPlayer() {
            localPlayer = this;
            LocalPlayerStarted?.Invoke();
        }

        public override void OnStopLocalPlayer() {
            LocalPlayerStopped?.Invoke();
        }
    }

}