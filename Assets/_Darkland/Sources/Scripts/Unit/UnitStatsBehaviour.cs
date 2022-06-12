using System;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats;
using _Darkland.Sources.Scripts.Unit.UnitStatsCounterHolder;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitStatsBehaviour : MonoBehaviour {

        private IUnitStatsProvider _unitStatsProvider;
        private UnitStats _unitStats;
        public event Action<UnitStats> Changed;

        private void Awake() {
            var unitStatsCounters = GetComponents<IUnitStatsCounterHolder>()
                    .Select(it => it.UnitStatsCounter);

            _unitStatsProvider = new UnitStatsProvider(unitStatsCounters);
        }

        [Server]
        public void ServerUpdateStats() {
            _unitStats = _unitStatsProvider.Get();
            Changed?.Invoke(_unitStats);
        }

        [Server]
        public UnitStats ServerGet() {
            return _unitStats;
        }
    }

}