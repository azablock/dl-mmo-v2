using System.Linq;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Movement {

    public static class MovementStaticObstacleFilter {

        [Server]
        public static bool ServerCanMove(WorldRootBehaviour world, Vector3Int current, Vector3Int target) {
            var chunkCoordinates = WorldRootBehaviour.ChunkCoordinatesByGlobalPos(target);
            var worldChunkBehaviour = world.worldChunks.FirstOrDefault(it => it.coordinates.Equals(chunkCoordinates));

            //is out of world bounds
            return !(worldChunkBehaviour == null || worldChunkBehaviour.StaticObstaclePositions.Contains(target));
        }
    }

}