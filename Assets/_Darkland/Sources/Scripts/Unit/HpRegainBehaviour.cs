using _Darkland.Sources.Models.Unit.Hp;
using _Darkland.Sources.ScriptableObjects.RegainStrategy;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(HpBehaviour))]
    public class HpRegainBehaviour : NetworkBehaviour {

        public RegainStrategy hpRegainStrategy;

        private HpBehaviour _hpBehaviour;
        private IRegainController _hpRegainHolder;

        private void Awake() {
            _hpBehaviour = GetComponent<HpBehaviour>();
            _hpRegainHolder = new RegainController();
        }

        public override void OnStartServer() {
            InvokeRepeating(nameof(ServerRegainHp), 0.0f, 1.0f);
        }

        public override void OnStopServer() {
            CancelInvoke();
        }

        [Server]
        private void ServerRegainHp() {
            var regainRate = hpRegainStrategy.Get(gameObject);
            var hpToRegain = _hpRegainHolder.GetRegain(regainRate);
            _hpBehaviour.ServerChangeHp((int) hpToRegain);
        }
    }

}