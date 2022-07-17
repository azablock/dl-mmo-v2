using System;
using System.Collections;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Unit.Stats2;
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
            
            Debug.Log($"{GetType()} - [Server] Set: {pos} [netId={netId}] (time={NetworkTime.time})");
        }

        [Server]
        public void ServerAdd(Vector3Int delta) {
            Set(Pos + delta);
        }

        [Client]
        private void ClientSyncPos(Vector3Int _, Vector3Int newPos) {
            StartCoroutine(ClientChangePosition(newPos, 1.0f));
        
            if (isLocalPlayer) {
                Debug.Log($"{GetType()}.ClientSyncPos(): local player current position: {newPos} [netId={netId}] (time={NetworkTime.time})");
            }
        }

        [Client]
        private IEnumerator ClientChangePosition(Vector3Int newPosition, float movementSpeed) {
            var oldTransformPosition = Vector3.zero + transform.position;
            var newTransformPosition = Vector3.zero + newPosition;
            var t = 0.0f;
        
            while (t < 1.0f) {
                transform.position = Vector3.Lerp(oldTransformPosition, newTransformPosition, t);
                t += movementSpeed * Time.deltaTime;
                yield return null;
            }
        
            transform.position = newTransformPosition;
        
            yield return null;
        }
    }

}