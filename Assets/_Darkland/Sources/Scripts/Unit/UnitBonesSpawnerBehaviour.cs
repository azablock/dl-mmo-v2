using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitBonesSpawnerBehaviour : MonoBehaviour {

        [SerializeField]
        private GameObject unitBonesPrefab;
        private IDiscretePosition _discretePosition;
        private IDeathEventEmitter _deathEventEmitter;

        [ServerCallback]
        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _deathEventEmitter = GetComponent<IDeathEventEmitter>();
        }

        [ServerCallback]
        private void OnEnable() {
            _deathEventEmitter.Death += ServerSpawnBones;
        }

        [ServerCallback]
        private void OnDisable() {
            _deathEventEmitter.Death -= ServerSpawnBones;
        }

        [Server]
        private void ServerSpawnBones() {
            var instance = Instantiate(unitBonesPrefab, _discretePosition.Pos, Quaternion.identity);
            NetworkServer.Spawn(instance);
        }
    }

}