using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Versioning {

    [CreateAssetMenu]
    public class GameVersion : ScriptableObject {

        public int major;
        public int minor;
        public int patch;
        public ChangeLog changeLog;

    }

}