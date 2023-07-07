using System;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IStatPostChangeHooksHandler {

        IStatsHolder StatsHolder { get; }
        StatPostChangeHook[] StatPostChangeHooks { get; }

        void Register();

        void Unregister();

    }

    public class StatPostChangeHooksHandler : IStatPostChangeHooksHandler {

        public StatPostChangeHooksHandler(IStatsHolder statsHolder, StatPostChangeHook[] statPostChangeHooks) {
            StatsHolder = statsHolder;
            StatPostChangeHooks = statPostChangeHooks;
        }

        public IStatsHolder StatsHolder { get; }
        public StatPostChangeHook[] StatPostChangeHooks { get; }

        public void Register() {
            foreach (var hook in StatPostChangeHooks) {
                var stat = StatsHolder.Stat(hook.onChangeStatId);
                stat.Changed += _ => hook.OnStatChange(StatsHolder);
            }
        }

        public void Unregister() {
            foreach (var hook in StatPostChangeHooks) {
                var stat = StatsHolder.Stat(hook.onChangeStatId);

                //todo jest jakis bug NPE na stat
                if (stat != null) stat.Changed -= statOnChanged(hook);
            }
        }

        private Action<StatVal> statOnChanged(StatPostChangeHook hook) {
            return _ => hook.OnStatChange(StatsHolder);
        }

    }

}