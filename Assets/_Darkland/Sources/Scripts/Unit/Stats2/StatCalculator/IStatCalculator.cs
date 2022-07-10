using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2.StatCalculator {

    public interface IMaxHealthStatValueCalculator<in T> {
        StatValue Calculate(T source);
    }

    public class FromStatsHolderMaxHealthStatValueCalculator : IMaxHealthStatValueCalculator<IStatsHolder> {

        public StatValue Calculate(IStatsHolder source) {
            var (mightStat, constitutionStat) = source.Stats(StatId.Might, StatId.Constitution);

            return StatValue.OfBasic(mightStat.Current + constitutionStat.Current);
        }
    }

    public class FromEquipmentMaxHealthStatValueCalculator : IMaxHealthStatValueCalculator<object> {

        public StatValue Calculate(object source) {
            return StatValue.Zero;
        }
    }

    public class MaxHealthStatValueCalculatorBehaviour : MonoBehaviour {
        
    }

}