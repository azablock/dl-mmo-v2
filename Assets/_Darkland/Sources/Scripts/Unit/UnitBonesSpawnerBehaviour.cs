using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitBonesSpawnerBehaviour : MonoBehaviour {

        [SerializeField]
        private GameObject unitBonesPrefab;
        private DarklandUnitDeathBehaviour _darklandUnitDeathBehaviour;
        private IDiscretePosition _discretePosition;

        [ServerCallback]
        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _darklandUnitDeathBehaviour = GetComponent<DarklandUnitDeathBehaviour>();
        }

        [ServerCallback]
        private void OnEnable() {
            _darklandUnitDeathBehaviour.ServerAddDeathCallback(ServerSpawnBones);
        }

        [ServerCallback]
        private void OnDisable() {
            _darklandUnitDeathBehaviour.ServerRemoveDeathCallback(ServerSpawnBones);
        }

        [Server]
        private void ServerSpawnBones() {
            var instance = Instantiate(unitBonesPrefab, _discretePosition.Pos, Quaternion.identity);
            NetworkServer.Spawn(instance);
        }

    }

}