using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                               () => (StatValue) fieldInfo.GetValue(statsHolder),
                               // () => ServerWrapStatsApi.ServerGet(statsHolder, statId),
                               val => {
                                   var valAfterConstraints = statsHolder
                                                             .StatConstraints(statId)
                                                             .Aggregate(val,
                                                                 (stat, constraint) => constraint.Apply(statsHolder, stat)
                                                             );

                                   fieldInfo.SetValue(statsHolder, valAfterConstraints);
                                   // ServerWrapStatsApi.ServerSet(statsHolder, statId, valAfterConstraints);
                               }
                           );
                       }

                   )
                   .ToList();
        }

        private static class ServerWrapStatsApi {

            [Server]
            public static void ServerSet(IStatsHolder statsHolder, StatId statId, StatValue statValue) {
                statsHolder.Stat(statId).Set(statValue);
            }

            [Server]
            public static StatValue ServerGet(IStatsHolder statsHolder, StatId statId) {
                return statsHolder.StatValue(statId);
            }
        }


        /*
             control.GetType()
           .GetMethods(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
           .Where(method => method.GetCustomAttribute<RPC>() == null)
           .Select(method => method.Name)
           .ToList()
           .ForEach(Console.WriteLine);
         */
    }

}