using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

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

        private float _randomOffset;

        private void Awake() {
            _randomOffset = Random.Range(0.0f, 0.5f);
        }

        private void FixedUpdate() {
            var perlinNoise = Mathf.PerlinNoise(Time.time + _randomOffset, 0);

            if (perlinNoise < noiseChance) {
                pointLight.pointLightOuterRadius = baseOuterRadius + perlinNoise * noiseIntensity;
            }
        }

    }

}