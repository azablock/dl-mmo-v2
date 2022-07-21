using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    //todo was MonoBehaviour
    public class DiscretePositionBehaviour : NetworkBehaviour, IDiscretePosition {

        //todo tmp
        public Vector3Int startPos;
        
        //todo czy jak bedzie interest management, to wtedy sie zrobi init na kliencie na nowo "widocznym" obiekcie?
        [field: SyncVar]
        // [field: SyncVar(hook = nameof(ClientSyncPos))]
        public Vector3Int Pos { get; private set; }

        public event Action<Vector3Int> Changed;

        public override void OnStartServer() {
            // Set(startPos);
        }

        [Server]
        public void Set(Vector3Int pos) {
            Pos = pos;
            Changed?.Invoke(Pos);
            
            Debug.LogWarning($"Server changed discrete pos " + NetworkTime.time);
        }

        [Server]
        public void ServerAdd(Vector3Int delta) {
            Set(Pos + delta);
        }
    }

}