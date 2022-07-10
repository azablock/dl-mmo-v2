using System;
using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.DiscretePosition {

    public class DiscretePositionBehaviour : MonoBehaviour, IDiscretePosition {

        //todo czy jak bedzie interest management, to wtedy sie zrobi init na kliencie na nowo "widocznym" obiekcie?
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
    }

}