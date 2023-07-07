using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Presentation {

    [CreateAssetMenu(fileName = nameof(ColorSet), menuName = "DL/" + nameof(ColorSet))]
    public class ColorSet : ScriptableObject {

        [Header("Main")]
        public Color primary;
        public Color secondary;
        public Color info;
        public Color success;
        public Color warning;
        public Color danger;
        public Color light;
        public Color dark;

    }

}