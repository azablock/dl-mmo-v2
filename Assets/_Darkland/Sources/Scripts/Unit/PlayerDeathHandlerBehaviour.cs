using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(
        typeof(HpBehaviour),
        typeof(DiscretePositionBehaviour))]
    public class PlayerDeathHandlerBehaviour : NetworkBehaviour {
        
        private HpBehaviour _hpBehaviour;
        private DiscretePositionBehaviour _discretePositionBehaviour;
        private IPlayerDeathEventEmitter _playerDeathEventEmitter;

        private void Awake() {
            _hpBehaviour = GetComponent<HpBehaviour>();
            _discretePositionBehaviour = GetComponent<DiscretePositionBehaviour>();
            _playerDeathEventEmitter = new PlayerDeathEventEmitter(_hpBehaviour.hpEventsHolder);
        }

        public override void OnStartServer() {
            _playerDeathEventEmitter.PlayerDead += ServerOnPlayerDead;
        }

        public override void OnStopServer() {
            _playerDeathEventEmitter.PlayerDead -= ServerOnPlayerDead;
        }

        [Server]
        private void ServerOnPlayerDead() {
            _hpBehaviour.ServerRegainHpToMaxHp();
            _discretePositionBehaviour.ServerSet(Vector3Int.zero);
        }
    }

}