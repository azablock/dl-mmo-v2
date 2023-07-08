using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.ScriptableObjects.Stats2;

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
                .Set(StatId.Might, StatVal.OfBasic(traits.might.Basic))
                .Set(StatId.Constitution, StatVal.OfBasic(traits.constitution.Basic))
                .Set(StatId.Dexterity, StatVal.OfBasic(traits.dexterity.Basic))
                .Set(StatId.Intellect, StatVal.OfBasic(traits.intellect.Basic))
                .Set(StatId.Soul, StatVal.OfBasic(traits.soul.Basic));
        }
        
        public static UnitTraits TraitStatsValues(this IStatsHolder holder) {
            var traitStatIds = Tuple.Create(
                StatId.Might,
                StatId.Constitution,
                StatId.Dexterity,
                StatId.Intellect,
                StatId.Soul
            );

            var traitStatsValues = holder.Values(traitStatIds);

            return new UnitTraits {
                might = traitStatsValues.Item1,
                constitution = traitStatsValues.Item2,
                dexterity = traitStatsValues.Item3,
                intellect = traitStatsValues.Item4,
                soul = traitStatsValues.Item5,
            };
        }

        public static void SetSecondaryStatsValues(this IStatsHolder holder) {
            HeroStatsCalculator
                .statsFormulasPreImage
                .Keys
                .ToList()
                .ForEach(it => holder.Set(it, HeroStatsCalculator.ValueOf(it, holder)));
        }


        public static UnitSecondaryStats SecondaryStatsValues(this IStatsHolder holder) {
            return new UnitSecondaryStats {
                actionPower = holder.ValueOf(StatId.ActionPower),
                actionSpeed = holder.ValueOf(StatId.ActionSpeed),
                healthRegain = holder.ValueOf(StatId.HealthRegain),
                magicResistance = holder.ValueOf(StatId.MagicResistance),
                manaRegain = holder.ValueOf(StatId.ManaRegain),
                movementSpeed = holder.ValueOf(StatId.MovementSpeed),
                physicalResistance = holder.ValueOf(StatId.PhysicalResistance)
            };
        }

        public static float BasicVal(this IStatsHolder holder, StatId statId) => holder.ValueOf(statId).Basic;
        public static float BonusVal(this IStatsHolder holder, StatId statId) => holder.ValueOf(statId).Bonus;
        public static float CurrentVal(this IStatsHolder holder, StatId statId) => holder.ValueOf(statId).Current;

    }

}