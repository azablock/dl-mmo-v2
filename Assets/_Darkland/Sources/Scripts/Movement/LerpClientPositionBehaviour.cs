using System.Collections;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Movement {

    [RequireComponent(typeof(IStatsHolder), typeof(IDiscretePosition))]
    public class LerpClientPositionBehaviour : NetworkBehaviour {

        private IDiscretePosition _discretePosition;
        private Stat _movementSpeedStat;
        private Coroutine _lerpMovementCoroutine;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            _movementSpeedStat = GetComponent<IStatsHolder>().Stat(StatId.MovementSpeed);
            _discretePosition.Changed += ServerOnDiscretePositionChanged;
        }

        public override void OnStopServer() {
            _discretePosition.Changed -= ServerOnDiscretePositionChanged;
        }

        [Server]
        private void ServerOnDiscretePositionChanged(PositionChangeData data) {
            var isClientImmediate = data.clientImmediate;

            if (isClientImmediate) {
                ClientRpcImmediateChangePosition(data.pos);
            } else {
                ClientRpcLerpPosition(data.pos, _movementSpeedStat.Get());
            }
        }

        [ClientRpc]
        private void ClientRpcLerpPosition(Vector3Int newPosition, float movementSpeed) {
            _lerpMovementCoroutine = StartCoroutine(ClientChangePosition(newPosition, movementSpeed));
        }

        [ClientRpc]
        private void ClientRpcImmediateChangePosition(Vector3Int newPosition) {
            if (_lerpMovementCoroutine != null) StopCoroutine(_lerpMovementCoroutine);
            transform.position = newPosition;
        }

        [Client]
        private IEnumerator ClientChangePosition(Vector3Int newPosition, float movementSpeed) {
            var oldTransformPosition = Vector3.zero + transform.position;
            var newTransformPosition = Vector3.zero + newPosition;
            var t = 0.0f;

            while (t < 1.0f) {
                transform.position = Vector3.Lerp(oldTransformPosition, newTransformPosition, t);
                t += movementSpeed * Time.deltaTime;
                yield return null;
            }

            transform.position = newTransformPosition;

            yield return null;
        }
    }

}