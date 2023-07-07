using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiPathHolderBehaviour : MonoBehaviour {

        private List<Vector3Int> _currentPath;

        private IPathfinder _pathfinder;

        private void Awake() {
            _pathfinder = new AStarPathfinder();
            _currentPath = new List<Vector3Int>();
        }

        [Server]
        public void ServerSetPath(Vector3Int start, Vector3Int dest) {
            _currentPath = _pathfinder.Path(start, dest, DarklandWorldBehaviour._);
        }

        public Vector3Int NextPos() {
            Assert.IsFalse(IsPathEmpty());
            var nextPos = _currentPath[0];

            _currentPath.RemoveAt(0);

            return nextPos;
        }

        public bool IsPathEmpty() {
            return _currentPath.Count == 0;
        }

    }

}