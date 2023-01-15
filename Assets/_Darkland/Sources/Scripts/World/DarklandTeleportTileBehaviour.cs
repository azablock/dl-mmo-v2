using _Darkland.Sources.Models.Presentation;
using _Darkland.Sources.Models.World;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    // [ExecuteAlways]
    public class DarklandTeleportTileBehaviour : MonoBehaviour, IDarklandTeleportTile {

        [SerializeField]
        private Vector3Int delta;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        public Vector3Int posDelta => delta;
        public Vector3Int position => Vector3Int.RoundToInt(transform.position);

        // [ExecuteAlways]
        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(position);
            gameObject.name = $"Teleport Tile {position}";
        }
        //
        // [UnityEditor.Callbacks.DidReloadScripts]
        // private static void OnScriptsReloaded() {
        //     // do something
        //     spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(position);
        //     gameObject.name = $"Teleport Tile {position}";
        // }
        
    }

}