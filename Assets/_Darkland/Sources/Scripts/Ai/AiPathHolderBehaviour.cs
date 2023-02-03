using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiPathHolderBehaviour : MonoBehaviour {

        private IPathfinder _pathfinder;
        private List<Vector3Int> _currentPath;

        private void Awake() {
            _pathfinder = new AStarPathfinder();
            _currentPath = new List<Vector3Int>();
        }

        [Server]
        public void ServerSetPath(Vector3Int start, Vector3Int dest) {
            _currentPath = _pathfinder.Path(start, dest, DarklandWorldBehaviour._);
        }

        public Vector3Int NextPos() => _currentPath.FirstOrDefault();

        public bool IsPathEmpty() => _currentPath.Count == 0;

    }

}