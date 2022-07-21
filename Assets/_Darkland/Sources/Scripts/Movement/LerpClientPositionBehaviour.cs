using System;
using System.Collections;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Movement {

    public class LerpClientPositionBehaviour : NetworkBehaviour {

        private IDiscretePosition _discretePosition;
        private Stat _movementSpeedStat;


        private int _clientPosChangeIndex = 0;
        private float _clientMovementSpeed;
        private Vector3Int _clientMovementVector;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            _movementSpeedStat = GetComponent<IStatsHolder>().Stat(StatId.MovementSpeed);
            _discretePosition.Changed += ServerOnDiscretePositionChanged;
            GetComponent<MovementBehaviour>().MovementVectorChanged += OnMovementVectorChanged;
        }

        [Server]
        private void OnMovementVectorChanged(Vector3Int obj) {
            RpcUpdateMovementVector(obj);
        }

        [ClientRpc]
        private void RpcUpdateMovementVector(Vector3Int val) {
            _clientMovementVector = val;
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
            // StartCoroutine(ClientChangePosition(newPosition, movementSpeed));
            _clientMovementSpeed = movementSpeed;
        }

        [ClientCallback]
        private void Update() {
            // Debug.Log($"discrete pos {_discretePosition.Pos} {NetworkTime.time}");


            if (_clientMovementVector.magnitude == 0) {
                if (transform.position == _discretePosition.Pos) return;
                transform.position = Vector3.MoveTowards(transform.position, _discretePosition.Pos, _clientMovementSpeed * Time.deltaTime);
                return;
            }

            var position = transform.position;
            var nextPos = _clientMovementVector.x > 0 || _clientMovementVector.y > 0
                ? Vector3Int.FloorToInt(position)
                : Vector3Int.CeilToInt(position);
            nextPos += Vector3Int.FloorToInt(_clientMovementVector);

            position = Vector3.MoveTowards(position, nextPos, _clientMovementSpeed * Time.deltaTime);
            transform.position = position;
        }

        [Client]
        private IEnumerator ClientChangePosition(Vector3Int newPosition, float movementSpeed) {
            Debug.LogWarning($"Coroutine#{_clientPosChangeIndex} Client START change transform pos " + NetworkTime.time
            );

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
            Debug.LogWarning($"Coroutine#{_clientPosChangeIndex} Client STOP change transform pos " + NetworkTime.time);
            _clientPosChangeIndex++;
        }
    }

}