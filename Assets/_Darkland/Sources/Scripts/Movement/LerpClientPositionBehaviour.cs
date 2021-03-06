using System.Collections;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Movement {

    public class LerpClientPositionBehaviour : NetworkBehaviour {

        private IDiscretePosition _discretePosition;
        private Stat _movementSpeedStat;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _movementSpeedStat = GetComponent<IStatsHolder>().Stat(StatId.MovementSpeed);
        }
        
        public override void OnStartServer() {
            _discretePosition.Changed += ServerOnDiscretePositionChanged;
        }

        public override void OnStopServer() {
            _discretePosition.Changed -= ServerOnDiscretePositionChanged;
        }

        [Server]
        private void ServerOnDiscretePositionChanged(Vector3Int pos) {
            ClientRpcChangePosition(pos, _movementSpeedStat.Current);
        }

        [ClientRpc]
        private void ClientRpcChangePosition(Vector3Int newPosition, float movementSpeed) {
            StartCoroutine(ClientChangePosition(newPosition, movementSpeed));
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