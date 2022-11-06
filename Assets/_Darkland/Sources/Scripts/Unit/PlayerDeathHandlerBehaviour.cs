using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;
using _Darkland.Sources.Scripts.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(IStatsHolder))]
    public class PlayerDeathHandlerBehaviour : NetworkBehaviour {
        
        private IStatsHolder _statsHolder;
        private IStatEffectHandler _statEffectHandler;
        private IDeathEventEmitter _deathEventEmitter;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _deathEventEmitter = new DeathEventEmitter(_statsHolder.Stat(StatId.Health));
        }

        public override void OnStartServer() {
            _deathEventEmitter.Death += ServerOnDead;
        }

        public override void OnStopServer() {
            _deathEventEmitter.Death -= ServerOnDead;
        }

        [Server]
        private void ServerOnDead() {
            var maxHealthValue = _statsHolder.ValueOf(StatId.MaxHealth);
            _statEffectHandler.ApplyDirectEffect(new DirectStatEffect(maxHealthValue, StatId.Health));
        }
    }

}