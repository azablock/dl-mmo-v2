using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.World {

    public interface IDarklandWorld {
        List<Vector3Int> obstaclePositions { get; }
        List<Vector3Int> allFieldPositions { get; } //contains also obstaclePositions values
        List<IDarklandTeleportTile> teleportTiles { get; }
    }

}