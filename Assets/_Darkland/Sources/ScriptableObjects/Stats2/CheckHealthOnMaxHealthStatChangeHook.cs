using System;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public class CheckHealthOnMaxHealthStatChangeHook : DarklandStatChangeHook {

        public override void Register(IDarklandStatsHolder statsHolder) {
            var healthApi = statsHolder.Stat(DarklandStatId.Health);
            var maxHealthApi = statsHolder.Stat(DarklandStatId.MaxHealth);
            
            maxHealthApi.Changed += maxHealthApiOnChanged(healthApi);
        }

        public override void Unregister(IDarklandStatsHolder statsHolder) {
            var healthApi = statsHolder.Stat(DarklandStatId.Health);
            var maxHealthApi = statsHolder.Stat(DarklandStatId.MaxHealth);

            maxHealthApi.Changed -= maxHealthApiOnChanged(healthApi);
        }

        private Action<FloatStat> maxHealthApiOnChanged(DarklandStat health) {
            return stat => OnMaxHealthChanged(stat, health);
        }

        private void OnMaxHealthChanged(FloatStat stat, DarklandStat health) {
            
        }
    }

}