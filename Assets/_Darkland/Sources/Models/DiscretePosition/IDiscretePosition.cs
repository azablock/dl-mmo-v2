using System;
using UnityEngine;

namespace _Darkland.Sources.Models.DiscretePosition {

    public struct PositionChangeData {
        public Vector3Int pos;
        public bool clientImmediate;
    }
    
    public interface IDiscretePosition {
        Vector3Int Pos { get; }
        void Set(Vector3Int pos);
        void SetClientImmediate(Vector3Int pos);
        event Action<PositionChangeData> Changed;
    }

}