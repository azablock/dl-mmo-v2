using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class DarklandMob : NetworkBehaviour {

        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            _discretePosition.Set(Vector3Int.FloorToInt(transform.position));
        }
        
        
    }

}