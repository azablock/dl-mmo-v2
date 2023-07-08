using _Darkland.Sources.ScriptableObjects.Versioning;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class GameVersionText : MonoBehaviour {

        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private GameVersion gameVersion;

        private void Awake() => text.text = $"Version {gameVersion.major}.{gameVersion.minor}.{gameVersion.patch}";

    }

}