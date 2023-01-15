using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.PlayerCamera;
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

            ClientOnLocalPlayerPosChanged(DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos);
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
            if (Camera.main == null) return;

            var newCameraPosZ = LocalPlayerCameraPositionResolver.ResolveCameraPositionZ(DarklandWorldBehaviour._.allFieldPositions, pos);
            var cameraTransform = Camera.main.transform;
            var cameraPos = new Vector3(0, 0, newCameraPosZ + cameraOffsetZ);

            cameraTransform.localPosition = cameraPos;
        }
    }

}