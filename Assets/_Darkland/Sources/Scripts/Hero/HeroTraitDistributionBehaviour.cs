using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Hero {

    public class HeroTraitDistributionBehaviour : NetworkBehaviour, IHeroTraitDistribution {

        [field: SyncVar(hook = nameof(ClientSyncPoints))]
        public int pointsToDistribute { get; private set; }

        public event Action<int> ClientChanged;

        private IStatsHolder _statsHolder;
        private IXpHolder _xpHolder;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _xpHolder = GetComponent<IXpHolder>();
        }

        public override void OnStartServer() {
            _xpHolder.ServerLevelChanged += XpHolderOnServerLevelChanged;
        }

        public override void OnStopServer() {
            _xpHolder.ServerLevelChanged -= XpHolderOnServerLevelChanged;
        }

        [Server]
        public void Distribute(StatId traitStatId) {
            Assert.IsTrue(IHeroTraitDistribution.traitStatIds.Contains(traitStatId));
            
            if (pointsToDistribute <= 0) return;
            // Assert.IsTrue(pointsToDistribute > 0);

            SetPointsToDistribute(pointsToDistribute - 1);
            _statsHolder.Add(traitStatId, StatVal.OfBasic(1));
        }

        [Server]
        public void SetPointsToDistribute(int val) {
            Assert.IsTrue(val >= 0);
            pointsToDistribute = val;
        }

        [Server]
        private void XpHolderOnServerLevelChanged(int _) {
            SetPointsToDistribute(pointsToDistribute + IHeroTraitDistribution.TraitPointsPerLevel);
        }

        private void ClientSyncPoints(int _, int val) => ClientChanged?.Invoke(val);

    }

}