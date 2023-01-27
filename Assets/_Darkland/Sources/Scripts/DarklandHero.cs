using System;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Scripts.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandHero : NetworkBehaviour {

        private UnitNameBehaviour _unitNameBehaviour;
        public HeroVocationType vocationType { get; private set; }

        public static DarklandHero localHero;
        public static event Action LocalHeroStarted;
        public static event Action LocalHeroStopped;

        private void Awake() {
            _unitNameBehaviour = GetComponent<UnitNameBehaviour>();
        }

        public override void OnStartServer() {
            _unitNameBehaviour.ServerUnitNameChanged += ServerTagGameObjectName;
        }

        public override void OnStopServer() {
            _unitNameBehaviour.ServerUnitNameChanged -= ServerTagGameObjectName;
            
            DarklandHeroService.ServerSaveDarklandHero(gameObject);
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
        public void ServerSetVocation(HeroVocationType v) => vocationType = v;

        [Server]
        private void ServerTagGameObjectName(string unitName) {
            gameObject.name += $" ({unitName} [netId={netId}])";
        }

    }

}