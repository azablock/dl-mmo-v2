using System;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IStatPostChangeHooksHandler {
        void Register();
        void Unregister();
        IStatsHolder StatsHolder { get; }
        StatPostChangeHook[] StatPostChangeHooks { get; }
    }

    public class StatPostChangeHooksHandler : IStatPostChangeHooksHandler {

        public IStatsHolder StatsHolder { get; }
        public StatPostChangeHook[] StatPostChangeHooks { get; }

        public StatPostChangeHooksHandler(IStatsHolder statsHolder, StatPostChangeHook[] statPostChangeHooks) {
            StatsHolder = statsHolder;
            StatPostChangeHooks = statPostChangeHooks;
        }

        public void Register() {
            foreach (var hook in StatPostChangeHooks) {
                var stat = StatsHolder.Stat(hook.onChangeStatId);
                stat.Changed += _ => hook.OnStatChange(StatsHolder);
            }
        }

        public void Unregister() {
            foreach (var hook in StatPostChangeHooks) {
                var stat = StatsHolder.Stat(hook.onChangeStatId);
                stat.Changed -= statOnChanged(hook);
            }
        }

        private Action<float> statOnChanged(StatPostChangeHook hook) {
            return _ => hook.OnStatChange(StatsHolder);
        }
    }

}