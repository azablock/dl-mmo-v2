using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public class DarklandMobSpawner : NetworkBehaviour {

        public GameObject darklandMobPrefab;
        public float respawnTime;

        private GameObject _mob;

        public override void OnStartServer() {
            ServerRespawnMob();
        }

        [Server]
        private void ServerRespawnMob() {
            Assert.IsNull(_mob);
            
            _mob = Instantiate(darklandMobPrefab, transform);
            _mob.GetComponent<IDeathEventEmitter>().Death += ServerOnMobDeath;
            
            NetworkServer.Spawn(_mob);
        }

        [Server]
        private void ServerOnMobDeath() {
            _mob.GetComponent<IDeathEventEmitter>().Death -= ServerOnMobDeath;
            
            NetworkServer.Destroy(_mob);
            
            Invoke(nameof(ServerRespawnMob), respawnTime);
        }

    }

}