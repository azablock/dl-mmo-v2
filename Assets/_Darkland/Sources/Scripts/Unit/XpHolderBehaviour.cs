using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class XpHolderBehaviour : NetworkBehaviour {

        public int xp;

        public override void OnStartServer() {
            
        }

        public void ServerSet(int val) {
            xp = val;
        }
        
        [Server]
        public void ServerGain(int val) {
            ServerSet(xp + val);
        }

    }

}