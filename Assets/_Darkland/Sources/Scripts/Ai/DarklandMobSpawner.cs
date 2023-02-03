using _Darkland.Sources.Models.DiscretePosition;
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
            
            var pos = Vector3Int.FloorToInt(transform.position);
    
            _mob = Instantiate(darklandMobPrefab, pos, Quaternion.identity);
            _mob.GetComponent<IDiscretePosition>().Set(pos);
            _mob.GetComponent<SpawnPositionHolder>().ServerSet(pos);
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