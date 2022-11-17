using _Darkland.Sources.ScriptableObjects.Presentation;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandColorSet : MonoBehaviour {
        public ColorSet colorSet;
        private static DarklandColorSet _instance;

        private void Awake() => _instance = this;

        public static ColorSet _ => _instance.colorSet;
    }

}