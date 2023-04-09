using System.Collections;
using System.Collections.Generic;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.World;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Movement {

    public class MovementBehaviour : MonoBehaviour {

        private IDiscretePosition _discretePosition;
        private Vector3Int _movementVector;
        private Coroutine _movementCoroutine;
        private IStatsHolder _statsHolder;
        private bool _isReadyForNextMove = true;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _statsHolder = GetComponent<IStatsHolder>();
        }

        [Server]
        public void ServerSetMovementVector(Vector3Int movementVector) {
            _movementVector = movementVector;

            if (_movementVector.x != 0 && _movementVector.y != 0) {
                _movementVector *= new Vector3Int(1, 0, 1);
            }

            if (!_isReadyForNextMove) return;

            if (_movementCoroutine != null) {
                StopCoroutine(_movementCoroutine);
                _isReadyForNextMove = true;
            }

            _movementCoroutine = StartCoroutine(ServerMove());
        }

        [Server]
        public void ServerMoveOnceClientImmediate(Vector3Int movementVector) {
            var possibleNewPos = _discretePosition.Pos + movementVector;
            if (!DarklandWorldBehaviour._.IsEmptyField(possibleNewPos)) return;

            ServerSetDiscretePosition(possibleNewPos, true);
        }

        [Server]
        public void ServerMoveOnce(Vector3Int movementVector) {
            if (!_isReadyForNextMove) return;

            var possibleNewPos = _discretePosition.Pos + movementVector;
            if (!DarklandWorldBehaviour._.IsEmptyField(possibleNewPos)) return;

            StartCoroutine(ServerProcessMoveOnce(movementVector));
        }

        [Server]
        public bool ServerIsReadyForNextMove() => _isReadyForNextMove;

        [Server]
        private IEnumerator ServerMove() {
            while (_movementVector != Vector3Int.zero) {
                _isReadyForNextMove = false;
                ServerSetDiscretePosition(_discretePosition.Pos + _movementVector);
                yield return new WaitForSeconds(ServerTimeBetweenMoves());
                _isReadyForNextMove = true;
            }
        }

        [Server]
        private IEnumerator ServerProcessMoveOnce(Vector3Int posDelta) {
                _isReadyForNextMove = false;
                ServerSetDiscretePosition(_discretePosition.Pos + posDelta);
                yield return new WaitForSeconds(ServerTimeBetweenMoves());
                _isReadyForNextMove = true;
        }

        [Server]
        private void ServerSetDiscretePosition(Vector3Int pos, bool clientImmediate = false) {
            if (!DarklandWorldBehaviour._.IsEmptyField(pos)) return;

            _discretePosition.Set(pos, clientImmediate);

            if (NetworkManager.singleton.mode == NetworkManagerMode.ServerOnly) {
                transform.position = pos;
            }
        }

        [Server]
        private float ServerTimeBetweenMoves() => 1.0f / _statsHolder.ValueOf(StatId.MovementSpeed).Current;
    }

}