using System;
using _Darkland.Sources.Models;
using Mirror;
using Random = UnityEngine.Random;

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
            var isSingleName = Random.Range(0, 10) % 3 == 0;
            characterName = isSingleName ? $"{CharacterNames.RandomName()}" : $"{CharacterNames.RandomName()} {CharacterNames.RandomName()}";
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