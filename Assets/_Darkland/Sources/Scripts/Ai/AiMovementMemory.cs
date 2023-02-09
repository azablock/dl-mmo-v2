using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiMovementMemory : MonoBehaviour {

        private IAvailableMovesHolder _idleAvailableMovesHolder;
        private List<Vector3Int> _lastChasePath;

        [ServerCallback]
        private void Awake() {
            _idleAvailableMovesHolder = new SimpleAvailableMovesHolder();
        }

        [Server]
        public Vector3Int ServerNextIdleMoveDelta() => _idleAvailableMovesHolder.NextMoveDelta();

    }

}