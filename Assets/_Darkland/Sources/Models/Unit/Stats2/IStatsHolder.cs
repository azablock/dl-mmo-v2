using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatsHolder {
        IStatsHolder Set(StatId id, float val);
        Stat Stat(StatId id);
        float ValueOf(StatId id);
        Tuple<float, float> Values(StatId firstId, StatId secondId);
        Tuple<float, float, float> Values(Tuple<StatId, StatId, StatId> ids);
        Tuple<float, float, float, float> Values(Tuple<StatId, StatId, StatId, StatId> ids);
        Tuple<float, float, float, float, float> Values(Tuple<StatId, StatId, StatId, StatId, StatId> ids);
        Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId);
        HashSet<Stat> stats { get; }
        HashSet<StatId> statIds { get; }
        IStatPreChangeHooksHolder statPreChangeHooksHolder { get; }
        event Action<StatId, float> ClientChanged;
    }

    public static class SimpleStatsHolderFunctions {

        public struct TraitValues {

            public float might;
            public float constitution;
            public float dexterity;
            public float intellect;
            public float soul;

        }

        public static void SetTraitStats(this IStatsHolder holder, TraitValues values) {
            holder
                .Set(StatId.Might, values.might)
                .Set(StatId.Constitution, values.constitution)
                .Set(StatId.Dexterity, values.dexterity)
                .Set(StatId.Intellect, values.intellect)
                .Set(StatId.Soul, values.soul);
        }
        
        public static TraitValues TraitStatsValues(this IStatsHolder holder) {
            var traitStatIds = Tuple.Create(
                StatId.Might,
                StatId.Constitution,
                StatId.Dexterity,
                StatId.Intellect,
                StatId.Soul
            );

            var traitStatsValues = holder.Values(traitStatIds);

            return new TraitValues {
                might = traitStatsValues.Item1,
                constitution = traitStatsValues.Item2,
                dexterity = traitStatsValues.Item3,
                intellect = traitStatsValues.Item4,
                soul = traitStatsValues.Item5,
            };
        }

    }

}