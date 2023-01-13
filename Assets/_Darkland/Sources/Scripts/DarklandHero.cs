using System;
using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class DarklandHero : NetworkBehaviour {

        [SyncVar(hook = nameof(ClientSyncHeroName))]
        public string heroName;

        public event Action<string> ClientHeroNameSet;
        public static event Action LocalHeroStarted;
        public static event Action LocalHeroStopped;
        public static DarklandHero localHero;

        public override void OnStartLocalPlayer() {
            localHero = this;
            LocalHeroStarted?.Invoke();

        }

        public override void OnStopLocalPlayer() {
            LocalHeroStopped?.Invoke();
        }

        [Client]
        private void ClientSyncHeroName(string _, string currentHeroName) {
            ClientHeroNameSet?.Invoke(currentHeroName);
        }
    }

}