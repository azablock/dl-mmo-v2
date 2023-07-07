using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitBonesBehaviour : NetworkBehaviour {

        [SerializeField]
        private float lifeTimeSeconds;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        public override void OnStartServer() {
            Invoke(nameof(ServerRemoveSelf), lifeTimeSeconds);
        }

        [Server]
        private void ServerRemoveSelf() {
            NetworkServer.Destroy(gameObject);
        }

    }

}