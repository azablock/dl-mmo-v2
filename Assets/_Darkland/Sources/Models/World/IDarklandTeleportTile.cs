using UnityEngine;

namespace _Darkland.Sources.Models.World {

    public interface IDarklandTeleportTile {
        Vector3Int posDelta { get; }
        Vector3Int position { get; }
    }

}