using _Darkland.Sources.Models.Unit.Regain;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class HealthRegainBehaviour : NetworkBehaviour {

        private IStatEffectHandler _statEffectHandler;
        private IStatsHolder _statsHolder;
        private IRegainController _regainController;

        private void Awake() {
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _statsHolder = GetComponent<IStatsHolder>();
            _regainController = new RegainController();
        }

        public override void OnStartServer() {
            InvokeRepeating(nameof(ServerRegainHealth), 0.0f, 1.0f);
        }

        public override void OnStopServer() {
            CancelInvoke(nameof(ServerRegainHealth));
        }

        [Server]
        private void ServerRegainHealth() {
            var healthRegainRate = _statsHolder.StatValue(StatId.HealthRegain).Current;
            var regain = _regainController.GetRegain(healthRegainRate);
            var healthDelta = StatValue.OfBasic(regain);
            var healthDirectEffect = new DirectStatEffect(healthDelta, StatId.Health);

            _statEffectHandler.ApplyDirectEffect(healthDirectEffect);
        }

    }

}