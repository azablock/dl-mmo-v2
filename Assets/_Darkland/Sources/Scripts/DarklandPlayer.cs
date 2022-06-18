using System;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    [RequireComponent(typeof(IStatsHolder))]
    public class DarklandPlayer : NetworkBehaviour {

        public IStatsHolder StatsHolder { get; private set; }
        
        public static event Action LocalPlayerStarted;
        public static event Action LocalPlayerStopped;
        public static DarklandPlayer localPlayer;

        private void Awake() {
            StatsHolder = GetComponent<IStatsHolder>();
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