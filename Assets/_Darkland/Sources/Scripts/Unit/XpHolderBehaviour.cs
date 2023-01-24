using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Scripts.Unit {

    public class XpHolderBehaviour : NetworkBehaviour, IXpHolder {

        public int xp { get; private set; }
        public int level { get; private set; }

        public event Action<int> ClientXpChanged;
        public event Action<ExperienceLevelChangeEvent> ClientLevelChanged;

        private IDeathEventEmitter _deathEventEmitter;

        private void Awake() {
            _deathEventEmitter = GetComponent<IDeathEventEmitter>();
        }

        public override void OnStartServer() {
            _deathEventEmitter.Death += ServerOnDeath;
        }

        public override void OnStopServer() {
            _deathEventEmitter.Death -= ServerOnDeath;
        }

        public override void OnStartLocalPlayer() => CmdGetCurrentExperienceState();

        [Command]
        private void CmdGetCurrentExperienceState() => TargetRpcLevelChanged(ServerEvt());

        [Server]
        public void ServerSetXp(int val) {
            xp = val;
            TargetRpcXpChanged(xp);

            var nextLevelXp = ServerNextLevelXpCap();

            if (xp < nextLevelXp) return;

            level++;
            
            //todo TEMP TEMP TEMP TEMP TEMP
            var statId = Random.Range(0, 4) % 2 == 0 ? StatId.Might : StatId.Constitution;
            GetComponent<IStatsHolder>().Add(statId, 1);
            var unitName = GetComponent<UnitNameBehaviour>().unitName;
            var message = $"{unitName} gained {level} level (and his {statId.ToString()} increased)";
            NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage() {
                message = ChatMessagesFormatter.FormatServerLog(message)
            });
            //todo TEMP TEMP TEMP TEMP TEMP
            
            TargetRpcLevelChanged(ServerEvt());
        }

        [Server]
        public void ServerInit(int currentXp, int currentLevel) {
            xp = currentXp;
            level = currentLevel;
        }

        [Server]
        public void ServerGain(int val) => ServerSetXp(xp + val);

        [Server]
        public void ServerLose(int val) => ServerSetXp(xp - val);

        [Server]
        private void ServerOnDeath() {
            /*
                 next level cap     2000
                 current level cap  800
                 current xp         850
                 
                 calculate:
                 (2000 - 800)   = 1200
                 (850 - 800)    = 50
                 50 / 10        = 5
             */

            var nextLevelXpCap = ServerNextLevelXpCap();
            var currentLevelXpCap = LevelXpCap(level);
            var capDiff = nextLevelXpCap - currentLevelXpCap;
            var currentXpProgress = capDiff - xp;
            var xpDeltaOnDeath = currentXpProgress / 10;

            ServerLose(xpDeltaOnDeath);
        }
        
        [Server]
        private ExperienceLevelChangeEvent ServerEvt() => new() {
            nextLevelXp = ServerNextLevelXpCap(),
            level = level,
            currentXp = xp
        };

        [Server]
        private int ServerNextLevelXpCap() => LevelXpCap(level + 1);

        [TargetRpc]
        private void TargetRpcLevelChanged(ExperienceLevelChangeEvent e) => ClientLevelChanged?.Invoke(e);

        [TargetRpc]
        private void TargetRpcXpChanged(int val) => ClientXpChanged?.Invoke(val);

        private static int LevelXpCap(int lvl) => lvl == 1 ? 0 : (int)(Math.Exp(lvl) * 10);

    }

}