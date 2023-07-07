using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Versioning {

    [CreateAssetMenu]
    public class ChangeLog : ScriptableObject {

        [TextArea(10, 100)]
        public string features;
        [TextArea(10, 100)]
        public string changes;
        [TextArea(10, 100)]
        public string fixes;

    }

}