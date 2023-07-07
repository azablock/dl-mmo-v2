using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Regain;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    [Serializable]
    public struct StatRegainRelation {

        public StatId regainSourceStatId;
        public StatId regainTargetStatId;

    }

    [Serializable]
    public struct StatRegainState {

        public StatRegainRelation statRegainRelation;
        public RegainState regainState;

    }

    public class RegainBehaviour : NetworkBehaviour {

        [SerializeField]
        private List<StatRegainRelation> regainRelations;
        private IRegainApplier _regainApplier;
        private IStatEffectHandler _statEffectHandler;
        private List<StatRegainState> _statRegainStates;
        private IStatsHolder _statsHolder;

        [ServerCallback]
        private void Awake() {
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _statsHolder = GetComponent<IStatsHolder>();
            _regainApplier = new RegainApplier(_statEffectHandler, _statsHolder);

            _statRegainStates = new List<StatRegainState>();

            regainRelations
                .ForEach(it =>
                             _statRegainStates.Add(new StatRegainState {
                                 regainState = new RegainState(),
                                 statRegainRelation = it
                             }));
        }

        public override void OnStartServer() {
            InvokeRepeating(nameof(ServerApplyRegain), 0.0f, 1.0f);
        }

        public override void OnStopServer() {
            CancelInvoke(nameof(ServerApplyRegain));
        }

        [Server]
        private void ServerApplyRegain() {
            _statRegainStates.ForEach(it => _regainApplier.ApplyRegain(it));
        }

    }

}