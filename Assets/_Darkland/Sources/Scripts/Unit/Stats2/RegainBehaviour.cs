using _Darkland.Sources.Models.Unit.Regain;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class RegainBehaviour : NetworkBehaviour {

        private IStatEffectHandler _statEffectHandler;
        private IStatsHolder _statsHolder;
        private IRegainApplier _regainApplier;
        private StatRegainEntry _healthRegainEntry;

        private void Awake() {
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _statsHolder = GetComponent<IStatsHolder>();
            _regainApplier = new RegainApplier(_statEffectHandler, _statsHolder);
            _healthRegainEntry = new StatRegainEntry {
                regainState = new RegainState(),
                regainStatId = StatId.HealthRegain,
                applyRegainToStatId = StatId.Health
            };
        }

        public override void OnStartServer() {
            InvokeRepeating(nameof(ServerApplyRegain), 0.0f, 1.0f);
        }

        public override void OnStopServer() {
            CancelInvoke(nameof(ServerApplyRegain));
        }

        [Server]
        private void ServerApplyRegain() {
            _regainApplier.ApplyRegain(_healthRegainEntry);
        }
    }

}