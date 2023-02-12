using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public static class HeroStatsCalculator {

        public class StatModifiersDict : Dictionary<StatId, Func<float, float>> { }

        public class StatsFormulas : Dictionary<StatId, StatModifiersDict> { }

        //todo refactor this!!!!!!!
        public static readonly Dictionary<StatId, float> startingValues = new() {
            { StatId.MaxHealth, 10 },
            { StatId.HealthRegain, 0.5f },
            { StatId.MaxMana, 10 },
            { StatId.ManaRegain, 0.25f },
            { StatId.ActionPower, 1 },
            { StatId.ActionSpeed, 1 },
            { StatId.MagicResistance, 0 },
            { StatId.PhysicalResistance, 0 },
            // { StatId.MovementSpeed, 8 },
            { StatId.MovementSpeed, 2 },
        };

        public static readonly StatsFormulas statsFormulas = new() {
            {
                StatId.Might, new StatModifiersDict {
                    { StatId.MaxHealth, v => v * 2 },
                    { StatId.ActionPower, v => Mathf.FloorToInt(v / 3.0f) }
                }
            },
            {
                StatId.Constitution, new StatModifiersDict {
                    { StatId.MaxHealth, v => v * 5 },
                    { StatId.HealthRegain, v => v / 5.0f },
                    { StatId.MaxMana, v => v * 2 },
                    { StatId.PhysicalResistance, v => Mathf.FloorToInt(v / 5) },
                }
            },
            {
                StatId.Dexterity, new StatModifiersDict {
                    { StatId.MovementSpeed, v => v / 100.0f },
                    { StatId.ActionSpeed, v => v / 5.0f },
                    { StatId.PhysicalResistance, v => Mathf.FloorToInt(v / 10) },
                }
            },
            {
                StatId.Intellect, new StatModifiersDict {
                    { StatId.MaxMana, v => v * 2 },
                    { StatId.ManaRegain, v => v / 10.0f },
                    { StatId.ActionSpeed, v => v / 10.0f },
                }
            },
            {
                StatId.Soul, new StatModifiersDict {
                    { StatId.MaxMana, v => v * 5 },
                    { StatId.ManaRegain, v => v / 5.0f },
                    { StatId.HealthRegain, v => v / 10.0f },
                    { StatId.MagicResistance, v => Mathf.FloorToInt(v / 10) }
                }
            },
            
        };

        public static readonly StatsFormulas statsFormulasPreImage = PreImage();

        public static StatVal ValueOf(StatId targetStatId, IStatsHolder statsHolder) {
            var v = 0.0f;
            
            foreach (var (sourceStatId, modifierFn) in statsFormulasPreImage[targetStatId]) {
                var sourceStatValue = statsHolder.ValueOf(sourceStatId);
                var modifierValue = modifierFn(sourceStatValue.Current);

                v += modifierValue;
            }

            //todo refactor this!!!!!!!
            if (startingValues.ContainsKey(targetStatId)) {
                v += startingValues[targetStatId];
            }

            return StatVal.OfBasic(v);
        }

        private static StatsFormulas PreImage() {
            var reversed = new StatsFormulas();
            
            foreach (var (statId, statModifiersDict) in statsFormulas) {
                foreach (var key in statModifiersDict.Keys) {
                    if (!reversed.ContainsKey(key)) reversed.Add(key, new StatModifiersDict());
                    
                    reversed[key].Add(statId, statModifiersDict[key]);
                }
            }
            
            return reversed;
        }

    }

}