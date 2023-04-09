using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class SpawnPositionHolder : MonoBehaviour {

        [Server]
        public void ServerSet(Vector3Int pos) {
            spawnPos = pos;
        }

        public Vector3Int spawnPos { get; private set; }

    }

}