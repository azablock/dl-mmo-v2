using System;
using _Darkland.Sources.Models.World;
using Mirror;

namespace _Darkland.Sources.Scripts.World {

    public class GameWorldTimeBehaviour : NetworkBehaviour, IGameWorldTime {

        public TimeSpan now { get; private set; }

        public override void OnStartServer() {
            now = TimeSpan.Zero;
            InvokeRepeating(nameof(ServerSetTime), 0.0f, 1.0f);
        }

        [Server]
        private void ServerSetTime() {
            now += TimeSpan.FromSeconds(1.0f);
        }

        [Server]
        public float ServerGetCurrentSeconds() => now.Seconds;
    }

}