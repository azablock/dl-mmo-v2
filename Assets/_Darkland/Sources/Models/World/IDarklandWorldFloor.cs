using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.World {

    public interface IDarklandWorldFloor {
        List<Vector3Int> obstaclePositions { get; }
        List<Vector3Int> allFieldPositions { get; }
    }

}