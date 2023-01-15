using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class DarklandMobSpawner : NetworkBehaviour {

        public GameObject darklandMobPrefab;
        public float respawnTime;

        public override void OnStartServer() {
            
        }

        public override void OnStopServer() {
            
        }

    }

}