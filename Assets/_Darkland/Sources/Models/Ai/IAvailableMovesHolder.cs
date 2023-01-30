using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public interface IAvailableMovesHolder {
        Vector3Int NextMoveDelta();
    }

    public class SimpleAvailableMovesHolder : IAvailableMovesHolder {

        private readonly HashSet<Vector3Int> _deltas = new() {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right,
            Vector3Int.zero,
        };

        private readonly List<Vector3Int> _current = new();
        
        public Vector3Int NextMoveDelta() {
            var size = _current.Count;

            if (size == 0) {
                for (var i = 0; i < 5; i++) {
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