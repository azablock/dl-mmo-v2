using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.World {

    public class TorchLight : MonoBehaviour {

        [SerializeField]
        private Light2D pointLight;
        [SerializeField]
        private float baseOuterRadius;
        [SerializeField]
        private float noiseIntensity;
        [SerializeField]
        [Range(0, 1)]
        private float noiseChance;
        
        private void FixedUpdate() {
            var perlinNoise = Mathf.PerlinNoise(Time.time * Random.Range(0, 1), 0);

            if (perlinNoise < noiseChance) {
                pointLight.pointLightOuterRadius = baseOuterRadius + perlinNoise * noiseIntensity;
            }
        }

    }

}