using System;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class XpHolderBehaviour : NetworkBehaviour, IXpHolder {

        private DarklandUnitDeathBehaviour _darklandUnitDeathBehaviour;

        private void Awake() {
            _darklandUnitDeathBehaviour = GetComponent<DarklandUnitDeathBehaviour>();
        }

        [field: SyncVar]
        public int xp { get; private set; }

        [field: SyncVar(hook = nameof(ClientSyncLevel))]
        public int level { get; private set; }

        public event Action<int> ServerLevelChanged;
        public event Action<int> ClientXpChanged;
        public event Action<ExperienceLevelChangeEvent> ClientLevelChanged;

        public override void OnStartServer() {
            _darklandUnitDeathBehaviour.ServerAddDeathCallback(ServerOnDeath);
        }

        public override void OnStopServer() {
            _darklandUnitDeathBehaviour.ServerRemoveDeathCallback(ServerOnDeath);
        }

        [Server]
        public void ServerSetXp(int val) {
            xp = val;
            TargetRpcXpChanged(xp);

            var nextLevelXp = ServerNextLevelXpCap();

            if (xp < nextLevelXp) return;

            level++;
            ServerLevelChanged?.Invoke(level);
        }

        [Server]
        public void ServerInit(int currentXp, int currentLevel) {
            xp = currentXp;
            level = currentLevel;
        }

        [Server]
        public void ServerGain(int val) {
            ServerSetXp(xp + val);
        }

        [Server]
        public void ServerLose(int val) {
            ServerSetXp(xp - val);
        }

        [Server]
        private void ServerOnDeath() {
            /*
                 next level cap     2000
                 current level cap  800
                 current xp         1400
                 
                 calculate:
                 (2000 - 800)       = 1200
                 (1400 - 800)       = 600
                 600 / 1200         = 0.5 progress [0, 1]
                 
             */

            // var nextLevelXpCap = ServerNextLevelXpCap();
            // var currentLevelXpCap = LevelXpCap(level);
            // var capDiff = nextLevelXpCap - currentLevelXpCap;
            // var currentXpProgress = capDiff - xp;
            // var xpDeltaOnDeath = currentXpProgress / 10;

            ServerSetXp(Math.Max(IXpHolder.LevelXpCap(level), xp - ServerNextLevelXpCap() / 4));
        }

        [Server]
        private int ServerNextLevelXpCap() {
            return IXpHolder.LevelXpCap(level + 1);
        }

        [TargetRpc]
        private void TargetRpcXpChanged(int val) {
            ClientXpChanged?.Invoke(val);
        }

        [Client]
        private void ClientSyncLevel(int _, int __) {
            ClientLevelChanged?.Invoke(this.LevelEvt());
        }

    }

}