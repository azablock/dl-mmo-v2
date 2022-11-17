using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        [field: SyncVar]
        public Vector3Int Pos { get; private set; }

        public event Action<PositionChangeData> Changed;

        [Server]
        public void Set(Vector3Int pos) {
            Pos = pos;
            Changed?.Invoke(new PositionChangeData {pos = Pos, clientImmediate = false});
        }

        [Server]
        public void SetClientImmediate(Vector3Int pos) {
            Pos = pos;
            Changed?.Invoke(new PositionChangeData {pos = Pos, clientImmediate = true});
        }

        [Server]
        public void ServerAdd(Vector3Int delta) {
            Set(Pos + delta);
        }
    }

}