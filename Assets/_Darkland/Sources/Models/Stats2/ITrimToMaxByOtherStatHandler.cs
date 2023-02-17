using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Stats2 {

    public interface ITrimToMaxByOtherStatHandler {
        void Handle(IStatsHolder statsHolder, StatId trimmedStatId, StatId onChangeStatId);
    }

}