using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.World {

    public class LightChangeLayer : MonoBehaviour {

        private Light2D light2D;

        private void Awake() {
            light2D = GetComponent<Light2D>();
        }

        private void Update() {
            // if (Input.GetKeyDown(KeyCode.F1)) {
            //     FieldInfo fieldInfo = light2D.GetType()
            //                                  .GetField("m_ApplyToSortingLayers",
            //                                      BindingFlags.NonPublic | BindingFlags.Instance
            //                                  );
            //
            //     var layers = new[] {
            //         SortingLayer.NameToID("Level 0"),
            //     };
            //
            //     fieldInfo.SetValue(light2D, layers);
            // }
            //
            // if (Input.GetKeyDown(KeyCode.F2)) {
            //     FieldInfo fieldInfo = light2D.GetType()
            //                                  .GetField("m_ApplyToSortingLayers",
            //                                      BindingFlags.NonPublic | BindingFlags.Instance
            //                                  );
            //
            //     var layers = new[] {
            //         SortingLayer.NameToID("Level 1"),
            //     };
            //
            //     fieldInfo.SetValue(light2D, layers);
            // }
            //
            //
            // var light2DPointLightOuterRadius = Math.Sin(Time.time / 360.0f) * 15f;
            // // light2D.pointLightOuterRadius = (float) light2DPointLightOuterRadius;


        }
    }

}