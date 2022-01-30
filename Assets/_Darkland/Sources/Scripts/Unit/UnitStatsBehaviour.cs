using System.Linq;
using _Darkland.Sources.Models.Unit.Stats;
using _Darkland.Sources.Scripts.Unit.UnitStatsCounterHolder;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitStatsBehaviour : MonoBehaviour {

        private IUnitStatsProvider _unitStatsProvider;

        private void Awake() {
            var unitStatsCounters = GetComponents<IUnitStatsCounterHolder>()
                    .Select(it => it.UnitStatsCounter);

            _unitStatsProvider = new UnitStatsProvider(unitStatsCounters);
        }

        [Server]
        public UnitStats ServerGet() {
            return _unitStatsProvider.Get();
        } 
    }

}