using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Hero {

    [Serializable]
    public struct StatStartValue {

        public StatId statId;
        public float startValue;

    }

    public interface IHeroVocationStartingStats {

        List<StatStartValue> StartValues { get; }

    }
    
    [CreateAssetMenu(fileName = nameof(HeroVocationStartingStats), menuName = "DL/"  + nameof(HeroVocationStartingStats))]
    public class HeroVocationStartingStats : ScriptableObject, IHeroVocationStartingStats {

        [SerializeField]
        private List<StatStartValue> startValues;

        public List<StatStartValue> StartValues => startValues;

    }

}