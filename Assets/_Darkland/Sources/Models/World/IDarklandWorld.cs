using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace _Darkland.Sources.Models.World {

    public interface IDarklandWorld {
        List<Vector3Int> obstaclePositions { get; }
        List<Vector3Int> allFieldPositions { get; } //contains also obstaclePositions values
        List<IDarklandTeleportTile> teleportTiles { get; }
    }

    public static class DarklandWorldExtensions {

        public static bool IsEmptyField(this IDarklandWorld world, Vector3Int pos) {
            return world.allFieldPositions.Contains(pos) && !world.obstaclePositions.Contains(pos);
        }

        public static void IsEmptyField(this IDarklandWorld world, Vector3Int pos, out bool worldHasPosition, out bool isObstacle) {
            worldHasPosition = world.allFieldPositions.Contains(pos);
            isObstacle = world.obstaclePositions.Contains(pos);
        }

        [CanBeNull]
        public static IDarklandTeleportTile TeleportTile(this IDarklandWorld world, Vector3Int pos) {
            return world.teleportTiles.FirstOrDefault(tile => tile.position.Equals(pos));
        }

        public static bool IsLadderUp(this IDarklandWorld world, Vector3Int pos) {
            return world.teleportTiles.Any(it => it.position.Equals(pos) && it.posDelta.z == -1);
        }

        public static bool IsLadderDown(this IDarklandWorld world, Vector3Int pos) {
            return world.teleportTiles.Any(it => it.position.Equals(pos) && it.posDelta.z == 1);
        }

    }

}