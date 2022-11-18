using System.Linq;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.PlayerCamera {

    public class LocalPlayerCameraBehaviour : MonoBehaviour {

        [SerializeField]
        private float cameraOffsetZ = 0.1f;
        [SerializeField]
        private Vector3 cameraOfflinePosition = new(0, 0, -10.0f);

        private void Awake() {
            DarklandHero.LocalHeroStarted += OnLocalHeroStarted;
            DarklandHero.LocalHeroStopped += OnLocalHeroStopped;
        }

        private void OnDestroy() {
            DarklandHero.LocalHeroStarted -= OnLocalHeroStarted;
            DarklandHero.LocalHeroStopped -= OnLocalHeroStopped;
        }

        private void OnLocalHeroStarted() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged += ClientOnLocalPlayerPosChanged;

            if (Camera.main == null) return;

            var cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(DarklandHero.localHero.transform);
            cameraTransform.localPosition = new Vector3(0, 0, cameraTransform.position.z);
        }

        private void OnLocalHeroStopped() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnLocalPlayerPosChanged;

            if (Camera.main == null) return;

            var cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(null);
            cameraTransform.position = cameraOfflinePosition;
        }

        [Client]
        private void ClientOnLocalPlayerPosChanged(Vector3Int pos) {
            var positionsBelowPlayer = WorldRootBehaviour2
                                       ._
                                       .AllFieldPositions
                                       .Where(it => pos.x == it.x && pos.y == it.y) //z axis is "flipped"
                                       .Where(it => pos.z > it.z) //z axis is "flipped"
                                       .OrderBy(it => it.z)
                                       .ToList();

            if (Camera.main == null) return;

            var newCameraPosZ = positionsBelowPlayer.Count > 0 ? positionsBelowPlayer[0].z : -10.0f;
            var cameraTransform = Camera.main.transform;
            var cameraPos = new Vector3(0, 0, newCameraPosZ + cameraOffsetZ);

            cameraTransform.localPosition = cameraPos;
        }
    }

}