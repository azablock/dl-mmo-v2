using System;
using _Darkland.Sources.Models.Dice;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.DarklandHero;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Hero;
using _Darkland.Sources.Scripts.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandHeroBehaviour : NetworkBehaviour {

        private UnitNameBehaviour _unitNameBehaviour;
        private IXpHolder _xpHolder;
        private IStatsHolder _statsHolder;

        public HeroVocation heroVocation { get; private set; }
        
        public static DarklandHeroBehaviour localHero;
        public static event Action LocalHeroStarted;
        public static event Action LocalHeroStopped;

        private void Awake() {
            _unitNameBehaviour = GetComponent<UnitNameBehaviour>();
            _xpHolder = GetComponent<IXpHolder>();
            _statsHolder = GetComponent<IStatsHolder>();
        }

        public override void OnStartServer() {
            _unitNameBehaviour.ServerUnitNameChanged += ServerTagGameObjectName;
            _xpHolder.ServerLevelChanged += ServerOnLevelChanged;
        }
        
        public override void OnStopServer() {
            _unitNameBehaviour.ServerUnitNameChanged -= ServerTagGameObjectName;
            _xpHolder.ServerLevelChanged -= ServerOnLevelChanged;

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
        public void ServerSetVocation(HeroVocationType v) => heroVocation = HeroVocationsContainer._.Vocation(v);

        [Server]
        private void ServerOnLevelChanged(int _) {

            //todo itd.
            var mightTraitDist = heroVocation.LevelTraitDistribution.might;
            var mightDiceRoll = DiceRoll.Start().Dx(mightTraitDist.dice).Result() + mightTraitDist.modifier;
            _statsHolder.Add(StatId.Might, StatVal.OfBasic(mightDiceRoll));
        }

        [Server]
        private void ServerTagGameObjectName(string unitName) {
            gameObject.name += $" ({unitName} [netId={netId}])";
        }

    }

}