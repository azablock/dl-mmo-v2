using _Darkland.Sources.Models.Core;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class TransferManaMarker : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

    }

}