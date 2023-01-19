using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.PlayerCamera;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.PlayerCamera {

    public class LocalPlayerCameraBehaviour : MonoBehaviour {

        [SerializeField]
        private Vector3 cameraClosePosition = new(0, 0, -0.1f);
        [SerializeField]
        private Vector3 cameraDistantPosition = new(0, 0, -10.0f);

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
            //
            var playerPos = DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos;
            var cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(DarklandHero.localHero.transform);
            // cameraTransform.localPosition = new Vector3(0, 0, cameraTransform.position.z - playerPos.z);

            ClientOnLocalPlayerPosChanged(playerPos);
        }

        private void OnLocalHeroStopped() {
            DarklandHero.localHero.GetComponent<IDiscretePosition>().ClientChanged -= ClientOnLocalPlayerPosChanged;

            if (Camera.main == null) return;

            var cameraTransform = Camera.main.transform;
            cameraTransform.SetParent(null);
            cameraTransform.position = cameraDistantPosition;
        }

        [Client]
        private void ClientOnLocalPlayerPosChanged(Vector3Int pos) => ClientSetCameraPos(pos);

        [Client]
        private void ClientSetCameraPos(Vector3Int playerPos) {
            if (Camera.main == null) return;

            var allFieldPositions = DarklandWorldBehaviour._.allFieldPositions;
            var isTileAboveLocalPlayer = LocalPlayerCameraPositionResolver.IsTileAboveLocalPlayer(allFieldPositions, playerPos);
            var cameraTransform = Camera.main.transform;

            cameraTransform.localPosition = isTileAboveLocalPlayer ? cameraClosePosition : cameraDistantPosition;
        }
    }

}