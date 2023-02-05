using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatsHolder {
        IStatsHolder Set(StatId id, StatVal val);
        IStatsHolder Add(StatId id, StatVal val);
        IStatsHolder Subtract(StatId id, StatVal val);
        Stat Stat(StatId id);
        StatVal ValueOf(StatId id);
        Tuple<StatVal, StatVal> Values(StatId firstId, StatId secondId);
        Tuple<StatVal, StatVal, StatVal> Values(Tuple<StatId, StatId, StatId> ids);
        Tuple<StatVal, StatVal, StatVal, StatVal> Values(Tuple<StatId, StatId, StatId, StatId> ids);
        Tuple<StatVal, StatVal, StatVal, StatVal, StatVal> Values(Tuple<StatId, StatId, StatId, StatId, StatId> ids);
        Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId);
        HashSet<Stat> stats { get; }
        HashSet<StatId> statIds { get; }
        IStatPreChangeHooksHolder statPreChangeHooksHolder { get; }
        event Action<StatId, StatVal> ClientChanged;
    }

    public static class SimpleStatsHolderExtensions {

        public static void SetTraitStatsBasicValues(this IStatsHolder holder, UnitTraits traits) {
            holder
                .Set(StatId.Might, StatVal.OfBasic(traits.might))
                .Set(StatId.Constitution, StatVal.OfBasic(traits.constitution))
                .Set(StatId.Dexterity, StatVal.OfBasic(traits.dexterity))
                .Set(StatId.Intellect, StatVal.OfBasic(traits.intellect))
                .Set(StatId.Soul, StatVal.OfBasic(traits.soul));
        }
        
        public static UnitTraits TraitStatsBasicValues(this IStatsHolder holder) {
            var traitStatIds = Tuple.Create(
                StatId.Might,
                StatId.Constitution,
                StatId.Dexterity,
                StatId.Intellect,
                StatId.Soul
            );

            var traitStatsValues = holder.Values(traitStatIds);

            return new UnitTraits {
                might = traitStatsValues.Item1.Basic,
                constitution = traitStatsValues.Item2.Basic,
                dexterity = traitStatsValues.Item3.Basic,
                intellect = traitStatsValues.Item4.Basic,
                soul = traitStatsValues.Item5.Basic,
            };
        }

        public static UnitTraits TraitStatsCurrentValues(this IStatsHolder holder) {
            var traitStatIds = Tuple.Create(
                StatId.Might,
                StatId.Constitution,
                StatId.Dexterity,
                StatId.Intellect,
                StatId.Soul
            );

            var traitStatsValues = holder.Values(traitStatIds);

            return new UnitTraits {
                might = traitStatsValues.Item1.Current,
                constitution = traitStatsValues.Item2.Current,
                dexterity = traitStatsValues.Item3.Current,
                intellect = traitStatsValues.Item4.Current,
                soul = traitStatsValues.Item5.Current,
            };
        }

    }

}