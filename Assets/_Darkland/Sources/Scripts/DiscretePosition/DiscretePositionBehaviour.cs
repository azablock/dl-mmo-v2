using System;
using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<PosChangeData> Changed;
        public event Action<Vector3Int> ClientChanged;

        [Server]
        public void Set(Vector3Int pos, bool clientImmediate = false) {
            Pos = pos;
            Changed?.Invoke(new PosChangeData {pos = Pos, clientImmediate = clientImmediate});
        }

        [Client]
        private void ClientSyncPos(Vector3Int _, Vector3Int pos) {
            ClientChanged?.Invoke(pos);
        }
    }

}