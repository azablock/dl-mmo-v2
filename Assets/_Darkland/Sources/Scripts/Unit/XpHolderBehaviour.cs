using System;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class XpHolderBehaviour : NetworkBehaviour, IXpHolder {

        public int xp { get; private set; }
        public int level { get; private set; }

        public event Action<int> ClientXpChanged;
        public event Action<ExperienceLevelChangeEvent> ClientLevelChanged;

        public override void OnStartLocalPlayer() => CmdGetCurrentExperienceState();

        [Server]
        public void ServerSetXp(int val) {
            xp = val;
            TargetRpcXpChanged(xp);

            var nextLevelXp = ServerNextLevelXp();

            if (xp < nextLevelXp) return;

            level++;
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
        private ExperienceLevelChangeEvent ServerEvt() => new() {
            nextLevelXp = ServerNextLevelXp(),
            level = level,
            currentXp = xp
        };

        [Server]
        private int ServerNextLevelXp() => XpLevelCap(level + 1);

        [Command]
        private void CmdGetCurrentExperienceState() => TargetRpcLevelChanged(ServerEvt());

        [TargetRpc]
        private void TargetRpcLevelChanged(ExperienceLevelChangeEvent e) => ClientLevelChanged?.Invoke(e);

        [TargetRpc]
        private void TargetRpcXpChanged(int val) => ClientXpChanged?.Invoke(val);

        private static int XpLevelCap(int lvl) => (int) (Math.Exp(lvl) * 10);

    }

}