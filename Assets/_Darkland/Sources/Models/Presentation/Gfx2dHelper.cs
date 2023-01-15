using UnityEngine;

namespace _Darkland.Sources.Models.Presentation {

    public static class Gfx2dHelper {

        public static int SortingLayerIdByPos(Vector3 pos) => SortingLayer.NameToID($"Level {pos.z}");
    }

}