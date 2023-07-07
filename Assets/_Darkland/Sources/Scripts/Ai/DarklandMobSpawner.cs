using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public class DarklandMobSpawner : NetworkBehaviour {

        public GameObject darklandMobPrefab;

        private GameObject _mob;

        public override void OnStartServer() {
            ServerRespawnMob();
        }

        [Server]
        private void ServerRespawnMob() {
            Assert.IsNull(_mob);

            var spawnPos = Vector3Int.FloorToInt(transform.position);

            _mob = Instantiate(darklandMobPrefab, spawnPos, Quaternion.identity);
            _mob.GetComponent<IDiscretePosition>().Set(spawnPos);
            _mob.GetComponent<SpawnPositionHolder>().ServerSet(spawnPos);
            _mob.GetComponent<DarklandUnitDeathBehaviour>().ServerAddDeathCallback(ServerOnMobDeath);

            var mobDef = _mob.GetComponent<IMobDefHolder>().MobDef;
            _mob.GetComponent<IStatsHolder>()
                .Set(StatId.MaxHealth, StatVal.OfBasic(mobDef.MaxHealth))
                .Set(StatId.Health, StatVal.OfBasic(mobDef.MaxHealth))
                .Set(StatId.MovementSpeed, StatVal.OfBasic(mobDef.MovementSpeed))
                .Set(StatId.HealthRegain, StatVal.OfBasic(mobDef.HealthRegain));

            _mob.GetComponent<UnitNameBehaviour>().ServerSet(mobDef.MobName);

            NetworkServer.Spawn(_mob);
        }

        [Server]
        private void ServerOnMobDeath() {
            var respawnTime = _mob.GetComponent<IMobDefHolder>().MobDef.RespawnTime;
            _mob.GetComponent<DarklandUnitDeathBehaviour>().ServerRemoveDeathCallback(ServerOnMobDeath);

            NetworkServer.Destroy(_mob);

            Invoke(nameof(ServerRespawnMob), respawnTime);
        }

    }

}