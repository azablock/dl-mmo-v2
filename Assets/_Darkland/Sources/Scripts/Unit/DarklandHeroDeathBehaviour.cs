using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(DarklandUnitDeathBehaviour))]
    public class DarklandHeroDeathBehaviour : MonoBehaviour {

        private DarklandUnitDeathBehaviour _darklandUnitDeathBehaviour;
        private IDiscretePosition _discretePosition;

        private IStatsHolder _statsHolder;

        [ServerCallback]
        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _discretePosition = GetComponent<IDiscretePosition>();
            _darklandUnitDeathBehaviour = GetComponent<DarklandUnitDeathBehaviour>();
        }

        [ServerCallback]
        private void OnEnable() {
            _darklandUnitDeathBehaviour.ServerAddDeathCallback(ServerOnDeath);
        }

        [ServerCallback]
        private void OnDisable() {
            _darklandUnitDeathBehaviour.ServerRemoveDeathCallback(ServerOnDeath);
        }

        [Server]
        private void ServerOnDeath() {
            _statsHolder.Set(StatId.Health, StatVal.OfBasic(1));
            _statsHolder.Set(StatId.Mana, StatVal.OfBasic(0));
            _discretePosition.Set(Vector3Int.zero, true);
        }

    }

}