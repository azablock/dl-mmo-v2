using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.World;
using UnityEngine;

namespace _Darkland.Sources.Models.Combat {

    public static class CombatTargetUtil {

        public static bool TargetBresenhamVisible(Vector3Int currentPos, Vector3Int targetPos, IDarklandWorld world) {
            var bresenham3Dv2 = new Bresenham3Dv2(currentPos, targetPos);

            //NONE from given result is obstacle
            return !bresenham3Dv2
                .Result()
                .Any(it => world.obstaclePositions.Contains(it));
        }

    }

}