using System;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace _Darkland.Sources.Scripts.World {

    public class GlobalWorldLight2 : MonoBehaviour {

        [FormerlySerializedAs("lightColor")]
        [SerializeField]
        private Gradient lightColorGradient;

        [SerializeField]
        private GameObject light2D;

        private GameWorldTimeBehaviour _gameWorldTimeBehaviour;

        [FormerlySerializedAs("_color")]
        [SerializeField]
        private float color; 

        [SerializeField]
        private string timeOfDay; //only for debug purposes

        private int _clientPartOfDay;
        private int _clientFullDayLength;
        private bool _clientReady;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroWorldTimeReceived += WorldTimeReceived;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroWorldTimeReceived -= WorldTimeReceived;
        }

        [Client]
        private void WorldTimeReceived(int partOfDay, int fullDayLength) {
            _clientPartOfDay = partOfDay;
            _clientFullDayLength = fullDayLength;
            _clientReady = true;
        }

        [ClientCallback]
        private void FixedUpdate() {
            if (!_clientReady) return;
            
            _clientPartOfDay++;
            color = _clientPartOfDay / (float) _clientFullDayLength;
            light2D.GetComponent<Light2D>().color = lightColorGradient.Evaluate(color);

            _clientPartOfDay %= _clientFullDayLength;

            timeOfDay = $"{Mathf.Floor(color * 24)}:00";
        }
    }

}