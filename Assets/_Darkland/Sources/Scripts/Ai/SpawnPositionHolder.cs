using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class SpawnPositionHolder : MonoBehaviour {

        public Vector3Int spawnPos { get; private set; }

        [Server]
        public void ServerSet(Vector3Int pos) {
            spawnPos = pos;
        }

    }

}