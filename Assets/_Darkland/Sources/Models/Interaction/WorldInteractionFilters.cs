using _Darkland.Sources.Models.World;
using UnityEngine;

namespace _Darkland.Sources.Models.Interaction {

    public static class WorldInteractionFilters {

        public static bool IsEmptyField(IDarklandWorld world, Vector3Int pos) {
            return world.allFieldPositions.Contains(pos) && !world.obstaclePositions.Contains(pos);
        }

        public static void IsEmptyField(IDarklandWorld world, Vector3Int pos, out bool worldHasPosition, out bool isObstacle) {
            worldHasPosition = world.allFieldPositions.Contains(pos);
            isObstacle = world.obstaclePositions.Contains(pos);
        }
    }

}