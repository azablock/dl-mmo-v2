using _Darkland.Sources.Models.Unit.Regain;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IRegainApplier {
        public void ApplyRegain(StatRegainState statRegainState);
    }

    public class RegainApplier : IRegainApplier {

        private readonly IStatEffectHandler _statEffectHandler;
        private readonly IStatsHolder _statsHolder;

        public RegainApplier(IStatEffectHandler statEffectHandler,
                             IStatsHolder statsHolder) {
            _statEffectHandler = statEffectHandler;
            _statsHolder = statsHolder;
        }

        public void ApplyRegain(StatRegainState statRegainState) {
            var regainRate = _statsHolder.ValueOf(statRegainState.statRegainRelation.regainStatId);
            var regain = statRegainState.regainState.GetRegain(regainRate.Current);
            var directStatEffect = new DirectStatEffect(StatVal.OfBasic(regain), statRegainState.statRegainRelation.applyRegainToStatId);

            _statEffectHandler.ApplyDirectEffect(directStatEffect);
        }
    }

}