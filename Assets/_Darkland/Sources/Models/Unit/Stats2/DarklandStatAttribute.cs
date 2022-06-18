using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    [AttributeUsage(AttributeTargets.Field)]
    public class DarklandStatAttribute : PropertyAttribute {
        public StatId id;

        public DarklandStatAttribute(StatId id) {
            this.id = id;
        }
        // public string setter;
        // public string getter;
    }

}