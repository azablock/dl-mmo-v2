using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public interface IPathfinder {

        List<Vector3Int> Path(Vector3Int start, Vector3Int target);

    }

    public class AStarPathfinder : IPathfinder {

        public List<Vector3Int> Path(Vector3Int start, Vector3Int target) {
            return new List<Vector3Int>();
        }

    }

}