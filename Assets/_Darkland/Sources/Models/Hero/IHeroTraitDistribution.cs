using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Models.Hero {

    public interface IHeroTraitDistribution {

        void Distribute(StatId traitStatId);
        void SetPointsToDistribute(int val);
        int pointsToDistribute { get; }
        event Action<int> ClientChanged;

        const int TraitPointsPerLevel = 5;

        public static int PointToDistributeForHero(UnitTraits unitTraits, int heroLevel) {
            var summedTraitValues = unitTraits.might.Basic
                                    + unitTraits.constitution.Basic
                                    + unitTraits.dexterity.Basic
                                    + unitTraits.intellect.Basic
                                    + unitTraits.soul.Basic;
            var availableTraitPoints = TraitPointsPerLevel * heroLevel;
            var result = availableTraitPoints - Mathf.FloorToInt(summedTraitValues);
            
            Assert.IsTrue(result >= 0);
            
            return result;
        }

    }

}