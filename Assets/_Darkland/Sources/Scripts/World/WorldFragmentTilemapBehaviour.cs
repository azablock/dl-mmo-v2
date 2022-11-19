using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Darkland.Sources.Scripts.World {

    [RequireComponent(typeof(Tilemap))]
    public class WorldFragmentTilemapBehaviour : MonoBehaviour {

        private Tilemap _tilemap;

        public List<Vector3Int> staticObstaclePositions { get; private set; }
        public List<Vector3Int> allFieldPositions { get; private set; }

        private void Awake() {
            _tilemap = GetComponent<Tilemap>();
            staticObstaclePositions = new List<Vector3Int>();
            allFieldPositions = new List<Vector3Int>();

            for (var y = _tilemap.cellBounds.yMin; y < _tilemap.cellBounds.yMax; y++) {
                for (var x = _tilemap.cellBounds.xMin; x < _tilemap.cellBounds.xMax; x++) {
                    var pos = new Vector3Int(x, y, 0);
                    var hasTile = _tilemap.HasTile(pos);

                    if (!hasTile) continue;

                    var worldPos = pos + Vector3Int.FloorToInt(_tilemap.transform.position);

                    allFieldPositions.Add(worldPos);

                    if (_tilemap.GetTile(pos).name.StartsWith("o_")) {
                        staticObstaclePositions.Add(worldPos);
                    }
                }
            }
        }
    }

}