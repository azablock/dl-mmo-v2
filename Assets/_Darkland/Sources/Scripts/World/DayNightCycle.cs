using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.World {

    public class DayNightCycle : MonoBehaviour {

        [SerializeField]
        private Gradient lightColor;

        [SerializeField]
        private GameObject light2D;

        private GameWorldTimeBehaviour _gameWorldTimeBehaviour;

        public float speed = 1.0f;
        
        private int partOfDay = 0; //to 255 * 256 = 65536
        
        private void Awake() {
            // _gameWorldTimeBehaviour = GetComponent<GameWorldTimeBehaviour>();
        }

        private void Update() {
            partOfDay++;
            light2D.GetComponent<Light2D>().color = lightColor.Evaluate(partOfDay / 65536.0f * speed); //256 * 256

            // Debug.Log($"partOfDay {partOfDay}, light color: {light2D.GetComponent<Light2D>().color}");

            partOfDay %= 65536;
        }
    }

}