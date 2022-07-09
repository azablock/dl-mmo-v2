using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    [AttributeUsage(AttributeTargets.Field)]
    public class DarklandStatAttribute : PropertyAttribute {
        public StatId id;
        public string setter;
        // public string getter;

        public DarklandStatAttribute(StatId id, String setter) {
            this.id = id;
            this.setter = setter;
        }
    }

}