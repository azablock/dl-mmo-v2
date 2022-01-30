using _Darkland.Sources.Models.Unit.StatsCounter;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.UnitStatsCounterHolder {

    public class ByUnitTraitsUnitStatsCounterHolderBehaviour : MonoBehaviour, IUnitStatsCounterHolder {

        public IUnitStatsCounter UnitStatsCounter { get; private set; }

        [SerializeField]
        private UnitTraitsBehaviour unitTraitsBehaviour;

        private void Awake() {
            UnitStatsCounter = new ByUnitTraitsUnitStatsCounter(() => unitTraitsBehaviour.ServerCurrent());
        }
    }

}