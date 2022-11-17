using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldChunk {
        public Vector2Int coordinates;
    }

    public class WorldChunkBehaviour : MonoBehaviour {

        public Vector2Int coordinates;



        [SerializeField]
        private List<WorldFragmentTilemapBehaviour> worldFragmentTilemaps;

        public List<Vector3Int> StaticObstaclePositions { get; private set; }

        private void Awake() {
            StaticObstaclePositions = worldFragmentTilemaps
                                      .Select(it => it.staticObstaclePositions)
                                      .Aggregate((curr, next) => curr.Concat(next).ToList());
        }
    }

}