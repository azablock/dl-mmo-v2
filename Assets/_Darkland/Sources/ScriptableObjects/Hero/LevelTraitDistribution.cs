using System;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Hero {

    [Serializable]
    public struct TraitDist {

        public int dice;
        public int modifier;

    }

    //todo meh - lepiej jak gracz mogl wybrac co chce rozwinac - na podst. np. 5 punktow per level
    [CreateAssetMenu(fileName = nameof(LevelTraitDistribution), menuName = "DL/" + nameof(LevelTraitDistribution))]
    public class LevelTraitDistribution : ScriptableObject {

        public TraitDist might;
        public TraitDist constitution;
        public TraitDist dexterity;
        public TraitDist intellect;
        public TraitDist soul;

    }

}