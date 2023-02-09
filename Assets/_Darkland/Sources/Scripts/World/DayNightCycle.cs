using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace _Darkland.Sources.Scripts.World {

    public class DayNightCycle : MonoBehaviour {

        [FormerlySerializedAs("lightColor")]
        [SerializeField]
        private Gradient lightColorGradient;

        [SerializeField]
        private GameObject light2D;

        private GameWorldTimeBehaviour _gameWorldTimeBehaviour;

        public float speed = 1.0f;
        private int _partOfDay; //0 to 255 * 256 = 65536
        [SerializeField]
        private float _color; //[SerializeField] for inspector debug

        [SerializeField]
        private float scale = 256.0f;

        [SerializeField]
        private string timeOfDay;
        
        private void FixedUpdate() {
            _partOfDay++;
            _color = _partOfDay / scale * speed; //todo speed?
            light2D.GetComponent<Light2D>().color = lightColorGradient.Evaluate(_color);

            _partOfDay %= (int) scale;

            timeOfDay = $"{Mathf.Floor(_color * 24)}:00";
            
            
        }
    }

}