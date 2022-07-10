using System;
using UnityEngine;

namespace _Darkland.Sources.Models.DiscretePosition {

    public interface IDiscretePosition {
        Vector3Int Pos { get; }
        void Set(Vector3Int pos);
        event Action<Vector3Int> Changed;
    }

}