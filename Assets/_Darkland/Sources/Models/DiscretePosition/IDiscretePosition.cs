using System;
using UnityEngine;

namespace _Darkland.Sources.Models.DiscretePosition {

    public struct PositionChangeData {
        public Vector3Int pos;
        public bool clientImmediate;
    }
    
    public interface IDiscretePosition {
        Vector3Int Pos { get; }
        void Set(Vector3Int pos, bool clientImmediate = false);
        event Action<PositionChangeData> Changed;
        event Action<Vector3Int> ClientChanged;
    }

}