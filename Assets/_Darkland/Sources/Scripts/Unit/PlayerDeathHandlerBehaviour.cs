using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(HpBehaviour))]
    public class PlayerDeathHandlerBehaviour : NetworkBehaviour {
        
        private HpBehaviour _hpBehaviour;
        private IPlayerDeathEventEmitter _playerDeathEventEmitter;

        private void Awake() {
            _hpBehaviour = GetComponent<HpBehaviour>();
            _playerDeathEventEmitter = new PlayerDeathEventEmitter(_hpBehaviour);
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
        }
    }

}