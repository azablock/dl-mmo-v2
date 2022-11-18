using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldRootBehaviour : MonoBehaviour {

        public List<WorldChunkBehaviour> worldChunks;

        private const int LowestFloorPositionZ = -4;
        private const int HightestFloorPositionZ = 4;
        private const int ChunkSize = 64;

        public static WorldRootBehaviour _;

        private void Awake() => _ = this;

        public static Vector2Int ChunkCoordinatesByGlobalPos(Vector3Int pos) => new(pos.x / ChunkSize, pos.y / ChunkSize);

        public WorldChunkBehaviour ChunkByGlobalPos(Vector3Int pos) =>
            worldChunks.FirstOrDefault(it => it.coordinates.Equals(ChunkCoordinatesByGlobalPos(pos)));
    }

}