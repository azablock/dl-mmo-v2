using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class AvailableMovesDummyHandler : MonoBehaviour {

        private readonly HashSet<Vector3Int> _deltas = new() {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.zero,
        };

        private readonly List<Vector3Int> _current = new();

        [Client]
        public Vector3Int ClientNextMoveDelta() {
            var size = _current.Count;

            if (size == 0) {
                for (var i = 0; i < 10; i++) {
                    _current.AddRange(_deltas);
                }
            };

            var randomIndex = Random.Range(0, _current.Count - 1);
            var el = _current[randomIndex];
            _current.RemoveAt(randomIndex);
            
            return el;
        }
    }
    
    

}