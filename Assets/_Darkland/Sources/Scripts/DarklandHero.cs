using System;
using _Darkland.Sources.Scripts.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandHero : NetworkBehaviour {

        public static event Action LocalHeroStarted;
        public static event Action LocalHeroStopped;
        public static DarklandHero localHero;
        private UnitNameBehaviour _unitNameBehaviour;

        private void Awake() {
            _unitNameBehaviour = GetComponent<UnitNameBehaviour>();
        }

        public override void OnStartServer() {
            _unitNameBehaviour.ServerUnitNameChanged += ServerTagGameObjectName;
        }

        public override void OnStopServer() {
            _unitNameBehaviour.ServerUnitNameChanged -= ServerTagGameObjectName;
        }

        public override void OnStartLocalPlayer() {
            localHero = this;
            LocalHeroStarted?.Invoke();
        }

        public override void OnStopLocalPlayer() {
            LocalHeroStopped?.Invoke();
            localHero = null;
        }

        [Server]
        private void ServerTagGameObjectName(string unitName) {
            gameObject.name += $" ({unitName} [netId={netId}])";
        }

    }

}