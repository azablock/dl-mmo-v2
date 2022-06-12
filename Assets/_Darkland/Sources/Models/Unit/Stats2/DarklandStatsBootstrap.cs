using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public static class DarklandStatsBootstrap {

        public static IEnumerable<DarklandStat> Init(IDarklandStatsHolder darklandStatsHolder) {
            var type = darklandStatsHolder.GetType();

            return type
                .GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Select(it => it.GetCustomAttribute<DarklandStatAttribute>())
                .Where(it => it != null)
                .ToList()
                .Select(attribute => {
                        var getterMethodName = attribute.getter;
                        var setterMethodName = attribute.setter;
                        var statId = attribute.id;

                        var getterMethodInfo = type.GetMethod(getterMethodName);
                        var setterMethodInfo = type.GetMethod(setterMethodName);

                        Debug.Assert(getterMethodInfo != null, nameof(getterMethodInfo) + " != null");
                        Debug.Assert(setterMethodInfo != null, nameof(setterMethodInfo) + " != null");

                        var getterMethodInvoke = getterMethodInfo.Invoke(darklandStatsHolder, null);
                        
                        return new DarklandStat(
                            statId,
                            () => (FloatStat) getterMethodInvoke,
                            val => {
                                setterMethodInfo.Invoke(darklandStatsHolder, new object[] {val});
                            }
                        );
                    }
                )
                .ToList();
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