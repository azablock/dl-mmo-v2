using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldRootBehaviour : MonoBehaviour {

        public List<WorldChunkBehaviour> worldChunks;

        private const int LowestFloorPositionZ = -4;
        private const int HightestFloorPositionZ = 4;
        private const int ChunkSize = 64;

        public static WorldRootBehaviour _;

        private void Awake() => _ = this;


        public static Vector2Int ChunkCoordinatesByGlobalPos(Vector3Int pos) {
            return new Vector2Int(pos.x / ChunkSize, pos.y / ChunkSize);
        }
    }

}