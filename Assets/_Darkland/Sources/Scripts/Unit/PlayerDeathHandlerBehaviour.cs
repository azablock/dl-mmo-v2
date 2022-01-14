using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(
        // typeof(UnitHpBehaviour),
        typeof(DiscretePositionBehaviour))]
    public class PlayerDeathHandlerBehaviour : NetworkBehaviour {

        private PlayerDeathHandler _playerDeathHandler;
        
        private void Awake() {
            // _playerDeathHandler = new PlayerDeathHandler(
                // GetComponent<UnitHpBehaviour>().unitHp,
                // GetComponent<DiscretePositionBehaviour>().discretePosition
            // );
        }

        public override void OnStartServer() {
            _playerDeathHandler.PlayerKilled += ServerOnPlayerKilled;
        }

        public override void OnStopServer() {
            _playerDeathHandler.PlayerKilled -= ServerOnPlayerKilled;
        }

        [Server]
        private void ServerOnPlayerKilled() {
            // _playerDeathHandler.RespawnPlayer();
        }
    }

}