using System;
using _Darkland.Sources.Models.Unit.Hp;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IPlayerDeathHandler {
        event Action PlayerKilled;
        IHpHolder HpHolder { get; }
        DiscretePosition DiscretePosition { get; }
    }
    
    public sealed class PlayerDeathHandler : IPlayerDeathHandler {
        public event Action PlayerKilled;
        public IHpHolder HpHolder { get; }
        public DiscretePosition DiscretePosition { get; }

        public PlayerDeathHandler(IHpHolder hpHolder, DiscretePosition discretePosition) {
            HpHolder = hpHolder;
            DiscretePosition = discretePosition;
            
            // HpHolder.HpChanged += OnUnitHpChanged;
            PlayerKilled += OnPlayerKilled;
        }

        ~PlayerDeathHandler() {
            // HpHolder.HpChanged -= OnUnitHpChanged;
            PlayerKilled -= OnPlayerKilled;
        }

        private void OnPlayerKilled() {
            RespawnPlayer();
        }

        private void RespawnPlayer() {
            DiscretePosition.Set(Vector3Int.zero); //todo tmp .zero
            //todo regain hp to max
        }

        private void OnUnitHpChanged(int hp) {
            if (hp == 0) {
                PlayerKilled?.Invoke();
            }
        }
    }

}