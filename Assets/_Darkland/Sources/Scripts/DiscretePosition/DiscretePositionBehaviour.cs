using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    //todo was MonoBehaviour
    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        //todo czy jak bedzie interest management, to wtedy sie zrobi init na kliencie na nowo "widocznym" obiekcie?
        [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<Vector3Int> Changed;

        [Server]
        public void Set(Vector3Int pos) {
            Pos = pos;
            Changed?.Invoke(Pos);
        }

        [Server]
        public void ServerAdd(Vector3Int delta) {
            Set(Pos + delta);
        }

        [Client]
        private void ClientSyncPos(Vector3Int _, Vector3Int newPos) {
            Debug.Log($"Client sync pos: {newPos} [netId={netId}] (time={NetworkTime.time})");
            transform.position = newPos;
        }
    }

}