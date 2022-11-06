using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        //todo tmp
        public Vector3Int startPos;
        
        [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<Vector3Int> Changed;

        public override void OnStartServer() {
            Set(startPos);
        }

        [Server]
        public void Set(Vector3Int pos) {
            Pos = pos;
            Changed?.Invoke(Pos);
        }

        [Server]
        public void ServerAdd(Vector3Int delta) {
            Set(Pos + delta);
        }

        private void ClientSyncPos(Vector3Int _, Vector3Int newPos) {
            Debug.Log($"netId[{netId}] changed pos on client: {newPos.ToString()}");
        }
    }

}