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
        private IPlayerDeathEventEmitter _playerDeathEventEmitter;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _playerDeathEventEmitter = new PlayerDeathEventEmitter(_statsHolder.Stat(StatId.Health));
        }

        public override void OnStartServer() {
            _playerDeathEventEmitter.PlayerDead += ServerOnPlayerDead;
        }

        public override void OnStopServer() {
            _playerDeathEventEmitter.PlayerDead -= ServerOnPlayerDead;
        }

        [Server]
        private void ServerOnPlayerDead() {
            var maxHealthValue = _statsHolder.StatValue(StatId.MaxHealth);
            _statEffectHandler.ApplyDirectEffect(new DirectStatEffect(maxHealthValue, StatId.Health));
        }
    }

}