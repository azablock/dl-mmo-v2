using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Models.PlayerCamera {

    public static class LocalPlayerCameraPositionResolver {

        public static float ResolveCameraPositionZ(IEnumerable<Vector3Int> allFieldPositions, Vector3Int localPlayerPos) {
            var positionsBelowPlayer = allFieldPositions
                                       .Where(it => localPlayerPos.x == it.x && localPlayerPos.y == it.y)
                                       .Where(it => localPlayerPos.z > it.z) //z axis is "flipped"
                                       .OrderBy(it => it.z)
                                       .ToList();

            return positionsBelowPlayer.Count > 0 ? positionsBelowPlayer[0].z : -10.0f;
        }
    }

}