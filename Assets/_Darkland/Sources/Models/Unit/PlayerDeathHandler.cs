using System;
using _Darkland.Sources.Models.Unit.Hp;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public class PlayerDeathHandler {
        public Action playerKilled;

        private readonly IUnitHpHolder _unitHpHolder;
        private readonly DiscretePosition _discretePosition;
        
        public PlayerDeathHandler(IUnitHpHolder unitHpHolder, DiscretePosition discretePosition) {
            _unitHpHolder = unitHpHolder;
            _discretePosition = discretePosition;
            
            //todo odnosimy sie do serwera
            _unitHpHolder.unitHpActions.serverMaxHpChanged += OnUnitHpChanged;
            playerKilled += OnPlayerKilled;
        }

        private void OnPlayerKilled() {
            RespawnPlayer();
        }

        private void RespawnPlayer() {
            _discretePosition.Set(Vector3Int.zero); //todo tmp .zero
            //todo regain hp to max
        }
        
        private void OnUnitHpChanged(int hp) {
            if (hp == 0) {
                playerKilled?.Invoke();
            }
        }
    }

}