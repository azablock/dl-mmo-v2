using _Darkland.Sources.Models.Unit.Regain;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IRegainApplier {
        public void ApplyRegain(StatRegainEntry statRegainEntry);
    }

    public class RegainApplier : IRegainApplier {

        private readonly IStatEffectHandler _statEffectHandler;
        private readonly IStatsHolder _statsHolder;

        public RegainApplier(IStatEffectHandler statEffectHandler,
                             IStatsHolder statsHolder) {
            _statEffectHandler = statEffectHandler;
            _statsHolder = statsHolder;
        }

        public void ApplyRegain(StatRegainEntry statRegainEntry) {
            var regainRate = _statsHolder.ValueOf(statRegainEntry.regainStatId);
            var regain = statRegainEntry.regainState.GetRegain(regainRate.Current);
            var directStatEffect = new DirectStatEffect(StatVal.OfBasic(regain), statRegainEntry.applyRegainToStatId);

            _statEffectHandler.ApplyDirectEffect(directStatEffect);
        }
    }

}