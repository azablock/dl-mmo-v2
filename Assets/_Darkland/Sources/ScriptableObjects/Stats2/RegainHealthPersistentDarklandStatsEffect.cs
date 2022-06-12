using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public class RegainHealthPersistentDarklandStatsEffect : PersistentDarklandStatsEffect {
        
        public override IEnumerator<float> Apply(IDarklandStatsHolder statsHolder) {
            var (health, healthRegain) = statsHolder.Stats(DarklandStatId.Health, DarklandStatId.HealthRegain);

            health.setAction(healthRegain.getFunc());

            yield return rate;
        }
    }

}