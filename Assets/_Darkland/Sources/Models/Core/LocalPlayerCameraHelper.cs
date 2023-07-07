using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Models.Core {

    public static class LocalPlayerCameraHelper {

        public static bool IsAnyTileAbovePos(IEnumerable<Vector3Int> allFieldPositions, Vector3Int pos) {
            return allFieldPositions
                .Where(it => pos.x == it.x && pos.y == it.y) //z axis is "flipped"
                .Any(it => pos.z > it.z);
        }
    }

}