using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.ScriptableObjects.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class LootHolderBehaviour : NetworkBehaviour {

        [SerializeField]
        private BasicWearableDef wearable;

        private DarklandUnitDeathBehaviour _darklandUnitDeathBehaviour;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _darklandUnitDeathBehaviour = GetComponent<DarklandUnitDeathBehaviour>();
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() => _darklandUnitDeathBehaviour.ServerAddDeathCallback(ServerDropLoot);

        public override void OnStopServer() => _darklandUnitDeathBehaviour.ServerRemoveDeathCallback(ServerDropLoot);

        [Server]
        private void ServerDropLoot() {
            if (wearable == null) return;
            OnGroundItemsManager._.ServerCreateOnGroundItem(_discretePosition.Pos, wearable.ItemName);
        }
    }

}