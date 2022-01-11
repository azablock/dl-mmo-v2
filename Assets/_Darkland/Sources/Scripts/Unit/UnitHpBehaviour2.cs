using _Darkland.Sources.Models.Unit.Hp;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitHpBehaviour2 : NetworkBehaviour, IUnitHpHolder {

        [field: SyncVar(hook = nameof(ClientSyncHp))]
        public int hp { get; private set; }

        [field: SyncVar(hook = nameof(ClientSyncMaxHp))]
        public int maxHp { get; private set; }

        public UnitHpActions unitHpActions { get; } = new UnitHpActions();

        public override void OnStartServer() {
            unitHpActions.serverMaxHpChanged += ServerOnMaxHpChanged;
        }

        public override void OnStopServer() {
            unitHpActions.serverMaxHpChanged -= ServerOnMaxHpChanged;
        }

        [Server]
        public void ServerChangeHp(int hpDelta) {
            hp = UnitHpCalculator.CalculateHp(this, hpDelta);
            unitHpActions.serverHpChanged?.Invoke(hp);
        }

        [Server]
        public void ServerChangeMaxHp(int maxHpDelta) {
            maxHp = UnitHpCalculator.CalculateMaxHp(this, maxHpDelta);
            unitHpActions.serverMaxHpChanged?.Invoke(maxHp);
        }

        [Server]
        private void ServerOnMaxHpChanged(int newMaxHp) {
            if (newMaxHp < hp) {
                ServerChangeHp(newMaxHp);
            }
        }

        [Client]
        private void ClientSyncHp(int _, int __) {
            unitHpActions.clientHpChanged?.Invoke(hp);
        }

        [Client]
        private void ClientSyncMaxHp(int _, int __) {
            unitHpActions.clientMaxHpChanged?.Invoke(hp);
        }
    }

}