using System;
using _Darkland.Sources.Models.Unit.Hp;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {
    
    public class HpBehaviour : NetworkBehaviour, IHpHolder {

        [field: SyncVar(hook = nameof(ClientSyncHp))]
        public int hp { get; private set; }
        [field: SyncVar(hook = nameof(ClientSyncMaxHp))]
        public int maxHp { get; private set; }
        public event Action<int> HpChanged;
        public event Action<int> MaxHpChanged;



        public event Action<int> ClientHpChanged;
        public event Action<int> ClientMaxHpChanged;

        private IHpCalculator _hpCalculator;
        
        private void Awake() {
            _hpCalculator = new HpCalculator();
        }

        public override void OnStartServer() {
            MaxHpChanged += ServerOnMaxHpChanged;
        }

        public override void OnStopServer() {
            MaxHpChanged -= ServerOnMaxHpChanged;
        }

        [Server]
        public void ChangeHp(int hpDelta) {
            hp = _hpCalculator.CalculateHp(this, hpDelta);
            HpChanged?.Invoke(hp);
        }

        [Server]
        public void ChangeMaxHp(int maxHpDelta) {
            maxHp = _hpCalculator.CalculateMaxHp(this, maxHpDelta);
            MaxHpChanged?.Invoke(maxHp);
        }
        
        [Server]
        public void ServerRegainHpToMaxHp() {
            ChangeHp(maxHp);
        }

        [Server]
        private void ServerOnMaxHpChanged(int newMaxHp) {
            if (newMaxHp >= hp) return;

            hp = maxHp;
            HpChanged?.Invoke(hp);
        }

        [Client]
        private void ClientSyncHp(int _, int newHp) {
            ClientHpChanged?.Invoke(newHp);
        }

        [Client]
        private void ClientSyncMaxHp(int _, int newMaxHp) {
            ClientMaxHpChanged?.Invoke(newMaxHp);
        }
    }

}