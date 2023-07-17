using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Rule {

    [Serializable]
    public struct Aaa {

        public StatId statId;
        public float val;

    }
    
    [CreateAssetMenu(fileName = nameof(DarklandRules), menuName = "DL/" + nameof(DarklandRules))]
    public class DarklandRules : ScriptableObject {

        public List<Aaa> startingStats;

    }

}