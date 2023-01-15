using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<PositionChangeData> Changed;
        public event Action<Vector3Int> ClientChanged;

        public bool readStartPosFromTransform;

        public override void OnStartServer() {
            if (readStartPosFromTransform) {
                Set(Vector3Int.FloorToInt(transform.position));
            }
        }

        [Server]
        public void Set(Vector3Int pos, bool clientImmediate = false) {
            Pos = pos;
            Changed?.Invoke(new PositionChangeData {pos = Pos, clientImmediate = clientImmediate});
        }

        [Client]
        private void ClientSyncPos(Vector3Int _, Vector3Int pos) {
            ClientChanged?.Invoke(pos);
        }
    }

}