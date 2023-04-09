using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Movement {

    public class StaticClientPositionBehaviour : NetworkBehaviour {

        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _discretePosition.Changed += ServerOnDiscretePositionChanged;
        }

        private void OnDestroy() {
            _discretePosition.Changed -= ServerOnDiscretePositionChanged;
        }

        [Server]
        private void ServerOnDiscretePositionChanged(PositionChangeData data) {
            ClientRpcChangePosition(data.pos);
        }

        [ClientRpc]
        private void ClientRpcChangePosition(Vector3Int pos) {
            transform.position = pos;
        }
    }

}