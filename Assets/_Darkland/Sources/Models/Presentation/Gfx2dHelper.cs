using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Models.Presentation {

    public static class Gfx2dHelper {

        public static int SortingLayerIdByPos(Vector3 pos) => SortingLayer.NameToID($"Level {pos.z}");

        public static void ApplyLight2dSortingLayer(Light2D light2D, Vector3Int pos) {
            var sortingLayerID = SortingLayerIdByPos(pos);
            var fieldInfo = light2D
                .GetType()
                .GetField("m_ApplyToSortingLayers", BindingFlags.NonPublic | BindingFlags.Instance);

            if (fieldInfo != null) fieldInfo.SetValue(light2D, new[] { sortingLayerID });
        }
    }

}