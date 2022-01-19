using System;
using _Darkland.Sources.Models.Unit.Hp;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {
    
    public class HpBehaviour : NetworkBehaviour {

        public event Action<int> ClientHpChanged;
        public event Action<int> ClientMaxHpChanged;
        
        private IHpController _hpController;

        private void Awake() {
            _hpController = new HpController(new HpHolder());
        }

        public override void OnStartServer() {
            _hpController.HpChanged += ClientRpcHpChanged;
            _hpController.MaxHpChanged += ClientRpcMaxHpChanged;
        }

        public override void OnStopServer() {
            _hpController.HpChanged -= ClientRpcHpChanged;
            _hpController.MaxHpChanged -= ClientRpcMaxHpChanged;
        }

        [Server]
        public void ServerChangeHp(int hpDelta) {
            _hpController.ChangeHp(hpDelta);
        }

        [Server]
        public void ServerChangeMaxHp(int maxHpDelta) {
            _hpController.ChangeMaxHp(maxHpDelta);
        }

        [Server]
        public void ServerRegainHpToMaxHp() {
            _hpController.RegainHpToMax();
        }

        [ClientRpc]
        private void ClientRpcHpChanged(int hp) {
            ClientHpChanged?.Invoke(hp);
        }

        [ClientRpc]
        private void ClientRpcMaxHpChanged(int maxHp) {
            ClientMaxHpChanged?.Invoke(maxHp);
        }

        public IHpEventsHolder hpEventsHolder => _hpController; 
    }

}