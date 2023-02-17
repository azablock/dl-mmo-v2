using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Stats2 {

    public class TrimToMaxByOtherStatHandler : ITrimToMaxByOtherStatHandler {

        public void Handle(IStatsHolder statsHolder, StatId trimmedStatId, StatId onChangeStatId) {
            var trimmedStat = statsHolder.Stat(trimmedStatId);
            var trimByStat = statsHolder.Stat(onChangeStatId);
    
            var trimmedStatCurrentValue = trimmedStat.Get();
            var trimByStatCurrentValue = trimByStat.Get();

            if (trimmedStatCurrentValue.Current > trimByStatCurrentValue.Current)
                trimmedStat.Set(StatVal.OfBasic(trimByStatCurrentValue.Current));
        }
    }

}