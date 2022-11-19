using System.Linq;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Movement {

    public static class MovementStaticObstacleFilter {

        [Server]
        public static bool ServerCanMove(WorldRootBehaviour2 world, Vector3Int current, Vector3Int target) {
            var worldHasPosition = world.AllFieldPositions.Any(it => it.Equals(target));
            var isNotObstacle = world.StaticObstaclePositions.All(it => !it.Equals(target));

            return worldHasPosition && isNotObstacle;
        }
    }

}