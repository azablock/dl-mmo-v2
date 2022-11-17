using System;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandHero : NetworkBehaviour {

        [SyncVar(hook = nameof(ClientSyncHeroName))]
        public string heroName;

        public event Action ClientStarted;
        public event Action<string> ClientHeroNameSet;
        public static event Action LocalHeroStarted;
        public static event Action LocalHeroStopped;
        public static DarklandHero localHero;

        public override void OnStartServer() {
            // var isSingleName = Random.Range(0, 10) % 3 == 0;
            // heroName = isSingleName ? $"{CharacterNames.RandomName()}" : $"{CharacterNames.RandomName()} {CharacterNames.RandomName()}";
        }

        public override void OnStartClient() {
            ClientStarted?.Invoke();
        }

        public override void OnStartLocalPlayer() {
            localHero = this;
            LocalHeroStarted?.Invoke();
            if (Camera.main != null) Camera.main.transform.SetParent(transform);
        }

        public override void OnStopLocalPlayer() {
            LocalHeroStopped?.Invoke();
            if (Camera.main != null) Camera.main.transform.SetParent(null);
        }

        [Client]
        private void ClientSyncHeroName(string _, string currentHeroName) {
            ClientHeroNameSet?.Invoke(currentHeroName);
        }
    }

}