using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldChunkBehaviour : MonoBehaviour {

        public Vector2Int coordinates;

        [SerializeField]
        private List<WorldFragmentTilemapBehaviour> worldFragmentTilemaps;

        public List<Vector3Int> StaticObstaclePositions { get; private set; }
        public List<Vector3Int> AllFieldPositions { get; private set; }

        private void Awake() {
            StaticObstaclePositions = worldFragmentTilemaps
                                      .Select(it => it.staticObstaclePositions)
                                      .Aggregate((curr, next) => curr.Concat(next).ToList());

            AllFieldPositions = worldFragmentTilemaps
                .Select(it => it.allFieldPositions)
                .Aggregate(new List<Vector3Int>(), (curr, next) => curr.Concat(next).ToList());
        }
    }

}