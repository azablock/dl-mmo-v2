using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    //todo was MonoBehaviour
    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        //todo czy jak bedzie interest management, to wtedy sie zrobi init na kliencie na nowo "widocznym" obiekcie?
        // [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<Vector3Int> Changed;

        public override void OnStartServer() {
            ClientRpcShowStartPosition(Pos);
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

        [ClientRpc]
        private void ClientRpcShowStartPosition(Vector3Int pos) {
            transform.position = pos;
        }
    }

}