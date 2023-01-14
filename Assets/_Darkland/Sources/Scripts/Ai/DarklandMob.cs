using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class DarklandMob : NetworkBehaviour {

        public override void OnStartServer() {
            GetComponent<IDiscretePosition>().Set(Vector3Int.FloorToInt(transform.position));
        }
    }

}