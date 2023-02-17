using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    [RequireComponent(typeof(DarklandUnitDeathBehaviour))]
    public class DarklandHeroDeathBehaviour : MonoBehaviour {

        private IStatsHolder _statsHolder;
        private IDiscretePosition _discretePosition;
        private IDeathEventEmitterHolder _deathEventEmitterHolder;

        [ServerCallback]
        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _discretePosition = GetComponent<IDiscretePosition>();
            _deathEventEmitterHolder = GetComponent<IDeathEventEmitterHolder>();
        }

        [ServerCallback]
        private void OnEnable() => _deathEventEmitterHolder.DeathEventEmitter.Death += ServerOnDeath;

        [ServerCallback]
        private void OnDisable() => _deathEventEmitterHolder.DeathEventEmitter.Death -= ServerOnDeath;

        [Server]
        private void ServerOnDeath() {
            _statsHolder.Set(StatId.Health, StatVal.OfBasic(1));
            _statsHolder.Set(StatId.Mana, StatVal.OfBasic(0));
            _discretePosition.Set(Vector3Int.zero, true);
        }
    }

}