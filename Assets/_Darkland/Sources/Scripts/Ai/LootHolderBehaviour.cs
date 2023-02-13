using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.ScriptableObjects.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class LootHolderBehaviour : NetworkBehaviour {

        [SerializeField]
        private BasicWearableDef wearable;

        private IDeathEventEmitter _deathEventEmitter;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            // _deathEventEmitter = GetComponent<IDeathEventEmitter>();
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            // _deathEventEmitter.Death += ServerDropLoot;
        }

        public override void OnStopServer() {
            if(wearable == null) return;
            OnGroundItemsManager._.ServerCreateOnGroundItem(_discretePosition.Pos, wearable.ItemName);
            // _deathEventEmitter.Death -= ServerDropLoot;
        }

        [Server]
        private void ServerDropLoot() {
            // if (wearable == null) return;
            // OnGroundItemsManager._.ServerCreateOnGroundItem(_discretePosition.Pos, wearable.ItemName);
        }
    }

}