using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mirror;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public static class DarklandStatsBootstrap {

        public static IEnumerable<Stat> Init(IStatsHolder statsHolder) {
            var type = statsHolder.GetType();
            var bindingFlags = BindingFlags.NonPublic
                               | BindingFlags.Public
                               | BindingFlags.Instance
                               | BindingFlags.DeclaredOnly;

            return type
                   .GetFields(bindingFlags)
                   .Where(fieldInfo => fieldInfo.GetCustomAttribute<DarklandStatAttribute>() != null)
                   .Select(fieldInfo => {
                           var attribute = fieldInfo.GetCustomAttribute<DarklandStatAttribute>();
                           var statId = attribute.id;

                           return new Stat(
                               statId,
                               // () => (StatValue) fieldInfo.GetValue(statsHolder),
                               () => ServerWrapStatsApi.ServerGet(() => (StatValue) fieldInfo.GetValue(statsHolder)),
                               val => {
                                   var valAfterConstraints = statsHolder
                                                             .StatConstraints(statId)
                                                             .Aggregate(val,
                                                                 (stat, constraint) => constraint.Apply(statsHolder, stat)
                                                             );

                                   // fieldInfo.SetValue(statsHolder, valAfterConstraints);
                                   ServerWrapStatsApi.ServerSet(() => fieldInfo.SetValue(statsHolder, valAfterConstraints));
                               }
                           );
                       }

                   )
                   .ToList();
        }

        private static class ServerWrapStatsApi {

            [Server]
            public static void ServerSet(Action setAction) {
                setAction();
            }

            [Server]
            public static StatValue ServerGet(Func<StatValue> getFunc) {
                return getFunc();
            }
        }
    }

}