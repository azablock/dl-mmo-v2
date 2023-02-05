using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mirror;
using UnityEngine;

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

                           var setterMethodName = attribute.setter;
                           var setterMethodInfo = type.GetMethod(setterMethodName, bindingFlags);

                           Debug.Assert(setterMethodInfo != null, nameof(setterMethodInfo) + " != null");

                           return new Stat(
                               statId,
                               () => (StatVal) fieldInfo.GetValue(statsHolder),
                               // () => ServerWrapStatsApi.ServerGet(() => (StatValue) fieldInfo.GetValue(statsHolder)),
                               val => {
                                   var statConstrainsHolder = statsHolder.statPreChangeHooksHolder;
                                   var hooks = statConstrainsHolder != null
                                       ? statConstrainsHolder.PreChangeHooks(statId)
                                       : new List<IStatPreChangeHook>();
                                   
                                   var valAfterConstraints = hooks
                                       .Aggregate(val, (stat, constraint) => constraint.Apply(statsHolder, stat));

                                   setterMethodInfo.Invoke(statsHolder, new object[] {valAfterConstraints});
                                   // ServerWrapStatsApi.ServerSet(() => setterMethodInfo.Invoke(statsHolder, new object[] {valAfterConstraints}));
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
            public static float ServerGet(Func<float> getFunc) {
                return getFunc();
            }
        }
    }

}