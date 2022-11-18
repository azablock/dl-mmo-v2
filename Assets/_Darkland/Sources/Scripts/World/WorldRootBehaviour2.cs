using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldRootBehaviour2 : MonoBehaviour {

        private List<WorldFragmentTilemapBehaviour> _worldFragmentTilemaps;

        public List<Vector3Int> StaticObstaclePositions { get; private set; }
        public List<Vector3Int> AllFieldPositions { get; private set; }

        public static WorldRootBehaviour2 _;
        
        private void Start() {
            _ = this;
            
            _worldFragmentTilemaps = GetComponentsInChildren<WorldFragmentTilemapBehaviour>().ToList();
            
            StaticObstaclePositions = _worldFragmentTilemaps
                                      .Select(it => it.staticObstaclePositions)
                                      .Aggregate((curr, next) => curr.Concat(next).ToList());

            AllFieldPositions = _worldFragmentTilemaps
                                .Select(it => it.allFieldPositions)
                                .Aggregate(new List<Vector3Int>(), (curr, next) => curr.Concat(next).ToList());
        }
    }

}