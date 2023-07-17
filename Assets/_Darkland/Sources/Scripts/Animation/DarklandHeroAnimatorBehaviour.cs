using System;
using _Darkland.Sources.Scripts.Movement;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Animation {

    public class DarklandHeroAnimatorBehaviour : NetworkBehaviour {

        [SerializeField]
        private MovementBehaviour movementBehaviour;

        public override void OnStartServer() {
            movementBehaviour.movementVectorChanged += ServerOnMovementVectorChanged;
        }

        private void ServerOnMovementVectorChanged(Vector3Int val) {
            // RpcSetAnim();
        }

    }

}