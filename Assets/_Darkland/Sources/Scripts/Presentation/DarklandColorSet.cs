using _Darkland.Sources.ScriptableObjects.Presentation;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandColorSet : MonoBehaviour {

        private static DarklandColorSet _instance;
        public ColorSet colorSet;

        public static ColorSet _ => _instance.colorSet;

        private void Awake() {
            _instance = this;
        }

    }

}