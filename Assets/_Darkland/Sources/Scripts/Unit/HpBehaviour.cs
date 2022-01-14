using System;
using _Darkland.Sources.Models.Unit.Hp;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {
    
    public class HpBehaviour : NetworkBehaviour {
        
        public event Action<int> ClientHpChanged;
        public event Action<int> ClientMaxHpChanged;
        private IHpHolder _hpHolder;

        private void Awake() {
            _hpHolder = new HpHolder();
        }

        public override void OnStartServer() {
            //todo pytanie jak to zrobic zeby bylo zachowanie jak "SyncVar"
            ClientRpcMaxHpChanged(_hpHolder.maxHp);
            ClientRpcHpChanged(_hpHolder.hp); 
            
            //todo somehow set initial hp/maxHp values - externally load from db
            
            _hpHolder.hpChanged += ServerOnHpChanged;
            _hpHolder.maxHpChanged += ServerOnMaxHpChanged;
        }

        public override void OnStopServer() {
            _hpHolder.hpChanged -= ServerOnHpChanged;
            _hpHolder.maxHpChanged -= ServerOnMaxHpChanged;
        }

        [Server]
        public void ServerChangeHp(int hpDelta) {
            _hpHolder.ChangeHp(hpDelta);
        }

        [Server]
        public void ServerChangeMaxHp(int maxHpDelta) {
            _hpHolder.ChangeMaxHp(maxHpDelta);
        }

        [Server]
        private void ServerOnHpChanged(int hp) {
            ClientRpcHpChanged(hp);
        }

        [Server]
        private void ServerOnMaxHpChanged(int maxHp) {
            ClientRpcMaxHpChanged(maxHp);
        }

        [ClientRpc]
        private void ClientRpcHpChanged(int hp) {
            ClientHpChanged?.Invoke(hp);
        }

        [ClientRpc]
        private void ClientRpcMaxHpChanged(int maxHp) {
            ClientMaxHpChanged?.Invoke(maxHp);
        }
    }

}