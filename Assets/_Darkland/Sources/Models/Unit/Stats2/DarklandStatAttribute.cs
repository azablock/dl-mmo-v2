using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    [AttributeUsage(AttributeTargets.Field)]
    public class DarklandStatAttribute : PropertyAttribute {
        public DarklandStatId id;
        public string setter;
        public string getter;
    }

}