using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public class HeroStatsCalculator : ScriptableObject {

        public class StatModifiersDict : Dictionary<StatId, Func<StatVal, StatVal>> { }

        public class StatsFormulas : Dictionary<StatId, StatModifiersDict> { }

        public static readonly StatsFormulas statsFormulas = new() {
            {
                StatId.Might, new StatModifiersDict {
                    { StatId.MaxHealth, v => StatVal.OfBasic(v.Current * 4) },
                    // { StatId.ActionPower, v => Mathf.Floor(v / 10) }
                }
            },
            {
                StatId.Constitution, new StatModifiersDict {
                    { StatId.MaxHealth, v => StatVal.OfBasic(v.Current * 10) },
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
                    { StatId.Might, v => StatVal.OfBasic(v.Current * 4) },
                    { StatId.Constitution, v => StatVal.OfBasic(v.Current * 10) }
                }
            }
        };

        
        //todo make private method of "reversed" statsFormulas (== remove statsFormulas2)
        public static StatVal ValueOf(StatId targetStatId, IStatsHolder statsHolder) {
            var v = StatVal.Zero;
            
            foreach (var (sourceStatId, modifierFn) in statsFormulas2[targetStatId]) {
                var sourceStatValue = statsHolder.ValueOf(sourceStatId);
                var modifierValue = modifierFn(sourceStatValue);

                v += modifierValue;
            }

            return v;
        }
        
    }

}