using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Darkland.Sources.Scripts.World {

    [RequireComponent(typeof(Tilemap))]
    public class WorldFragmentTilemapBehaviour : MonoBehaviour {

        public Tilemap tilemap { get; private set; }

        public List<Vector3Int> staticObstaclePositions { get; private set; }
        public List<Vector3Int> allFieldPositions { get; private set; }

        private void Awake() {
            tilemap = GetComponent<Tilemap>();
            staticObstaclePositions = new List<Vector3Int>();

            for (var y = 0; y < 64; y++) {
                for (var x = 0; x < 64; x++) {
                    var pos = new Vector3Int(x, y, 0);

                    var hasTile = tilemap.HasTile(pos);

                    if (hasTile) {
                        allFieldPositions.Add(pos);
                    }
                    
                    if (hasTile && tilemap.GetTile(pos).name.StartsWith("o_")) {
                        staticObstaclePositions.Add(pos);
                    }
                }
            }
        }
    }

}