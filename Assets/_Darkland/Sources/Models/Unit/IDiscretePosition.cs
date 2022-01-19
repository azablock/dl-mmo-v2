using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IDiscretePosition {
        Vector3Int position { get; }
        event Action<Vector3Int> Changed;
        void Set(Vector3Int pos);
    }

    public class DiscretePosition : IDiscretePosition {

        public Vector3Int position { get; private set; }
        public event Action<Vector3Int> Changed;

        public void Set(Vector3Int pos) {
            position += Vector3Int.zero + pos;
            Changed?.Invoke(position);
        }
    }
}