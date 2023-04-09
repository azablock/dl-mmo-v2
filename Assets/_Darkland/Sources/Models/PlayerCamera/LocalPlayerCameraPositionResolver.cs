using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Models.PlayerCamera {

    public static class LocalPlayerCameraPositionResolver {

        public static bool IsTileAboveLocalPlayer(IEnumerable<Vector3Int> allFieldPositions, Vector3Int localPlayerPos) {
            return allFieldPositions
                .Where(it => localPlayerPos.x == it.x && localPlayerPos.y == it.y) //z axis is "flipped"
                .Any(it => localPlayerPos.z > it.z);
        }
    }

}