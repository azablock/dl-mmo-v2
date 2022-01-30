using _Darkland.Sources.Models.Unit.Hp;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(HpBehaviour))]
    public class HpRegainBehaviour : NetworkBehaviour {

        private HpBehaviour _hpBehaviour;
        private IHpRegainHolder _hpRegainHolder;

        private void Awake() {
            _hpBehaviour = GetComponent<HpBehaviour>();
            _hpRegainHolder = new HpRegainHolderHolder();
        }

        public override void OnStartServer() {
            InvokeRepeating(nameof(ServerRegainHp), 0.0f, 1.0f);
        }

        public override void OnStopServer() {
            CancelInvoke();
        }

        [Server]
        private void ServerRegainHp() {
            _hpRegainHolder.IncrementHpRegain();

            var hpToRegain = _hpRegainHolder.ResolveHpToRegain();

            _hpBehaviour.ServerChangeHp(hpToRegain);
        }
    }

}