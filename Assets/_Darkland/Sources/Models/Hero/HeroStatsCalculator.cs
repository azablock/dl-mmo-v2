using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Models.Hero {

    public static class HeroStatsCalculator {

        public class StatModifiersDict : Dictionary<StatId, Func<float, float>> { }

        public class StatsFormulas : Dictionary<StatId, StatModifiersDict> { }


        // public struct StatModifierFormula {
        //
        //     public StatId sourceStatId;
        //     public StatModifiersDict modifiers;
        //
        //     public StatModifierFormula(StatId sourceStatId, StatModifiersDict modifiers) {
        //         Assert.IsFalse(modifiers.ContainsKey(sourceStatId));
        //
        //         this.sourceStatId = sourceStatId;
        //         this.modifiers = modifiers;
        //     }
        //
        // }

        public static readonly StatsFormulas statsFormulas = new() {
            {
                StatId.Might, new StatModifiersDict {
                    { StatId.MaxHealth, v => v * 3 },
                    // { StatId.ActionPower, v => Mathf.Floor(v / 10) }
                }
            },
            {
                StatId.Constitution, new StatModifiersDict {
                    { StatId.MaxHealth, v => v * 50 },
                }
            }
        };
        
        //////////////////////////////////////////

        /**
         * tutaj jest odwrotnie - ze klucz glowny to jest to co bedzie zmienione, a pozniej poadne zrodla
         */
        public static StatsFormulas statsFormulas2 = new() {
            {
                StatId.MaxHealth, new StatModifiersDict {
                    { StatId.Might, v => v * 3 },
                    { StatId.Constitution, v => v * 40 }
                }
            }
        };

        
        //todo make private method of "reversed" statsFormulas (== remove statsFormulas2)
        public static float ValueOf(StatId targetStatId, IStatsHolder statsHolder) {
            var v = 0.0f;
            
            foreach (var (sourceStatId, modifierFn) in statsFormulas2[targetStatId]) {
                var sourceStatValue = statsHolder.ValueOf(sourceStatId);
                var modifierValue = modifierFn(sourceStatValue);

                v += modifierValue;
            }

            return v;
        }
        
    }

}