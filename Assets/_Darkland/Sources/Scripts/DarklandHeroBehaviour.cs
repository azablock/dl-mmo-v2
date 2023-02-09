using System;
using _Darkland.Sources.Models.Dice;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.DarklandHero;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Hero;
using _Darkland.Sources.Scripts.Presentation.Unit;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandHeroBehaviour : NetworkBehaviour {

        [SerializeField]
        private DarklandUnitView heroView;
        private UnitNameBehaviour _unitNameBehaviour;

        public HeroVocation heroVocation { get; private set; }
        
        public static DarklandHeroBehaviour localHero;
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

        public override void OnStartClient() {
            heroView.unitSpriteRenderer.sprite = heroVocation.VocationSprite;
        }

        [Server]
        public void ServerSetVocation(HeroVocationType v) => heroVocation = HeroVocationsContainer._.Vocation(v);

        [Server]
        private void ServerTagGameObjectName(string unitName) {
            gameObject.name += $" ({unitName} [netId={netId}])";
        }

    }

}