using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.PlayerCamera {

    public class LocalPlayerCameraBehaviour : MonoBehaviour {

        private void Awake() {
            DarklandHero.LocalHeroStarted += ClientOnLocalHeroStarted;
            DarklandHero.LocalHeroStopped += ClientOnLocalHeroStopped;
        }

        private void OnDestroy() {
            DarklandHero.LocalHeroStarted -= ClientOnLocalHeroStarted;
        }

        [Client]
        private void ClientOnLocalHeroStarted() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnLocalPlayerPosChanged;
        }

        [Client]
        private void ClientOnLocalHeroStopped() {
        }

        [Client]
        public void ClientConnectToLocalPlayer() {
        }

        [Client]
        public void ClientDisconnectToLocalPlayer() {
            
        }

        [Client]
        private void ClientOnLocalPlayerPosChanged(Vector3Int pos) {
            var worldChunk = WorldRootBehaviour._.ChunkByGlobalPos(pos);
            
        }
    }

}