using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.World;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class DarklandWorldBehaviour : MonoBehaviour, IDarklandWorld {

        private List<DarklandWorldFloorBehaviour> _darklandWorldFloors;

        public List<Vector3Int> obstaclePositions { get; private set; }
        public List<Vector3Int> allFieldPositions { get; private set; }
        public List<IDarklandTeleportTile> teleportTiles { get; private set; }

        public static IDarklandWorld _;
        
        private void Start() {
            _ = this;
            
            // NetworkServer.Spawn();
           
            
            
            _darklandWorldFloors = GetComponentsInChildren<DarklandWorldFloorBehaviour>().ToList();
            
            obstaclePositions = _darklandWorldFloors
                                      .Select(it => it.obstaclePositions)
                                      .Aggregate((curr, next) => curr.Concat(next).ToList());

            allFieldPositions = _darklandWorldFloors
                                .Select(it => it.allFieldPositions)
                                .Aggregate(new List<Vector3Int>(), (curr, next) => curr.Concat(next).ToList());

            teleportTiles = GetComponentsInChildren<IDarklandTeleportTile>().ToList();
        }
    }

}