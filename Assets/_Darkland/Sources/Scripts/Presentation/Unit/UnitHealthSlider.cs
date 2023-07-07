using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class UnitHealthSlider : MonoBehaviour {

        [SerializeField]
        private Slider slider;
        private DarklandUnit _darklandUnit;
        private IStatsHolder _statsHolder;

        private void Awake() {
            _statsHolder = GetComponentInParent<IStatsHolder>();
            _darklandUnit = GetComponentInParent<DarklandUnit>();
            slider.minValue = 0;

        }

        private void OnEnable() {
            _statsHolder.ClientChanged += ClientOnStatsChanged;
            _darklandUnit.ClientStarted += DarklandUnitOnClientStarted;
        }

        private void OnDisable() {
            _statsHolder.ClientChanged -= ClientOnStatsChanged;
            _darklandUnit.ClientStarted -= DarklandUnitOnClientStarted;
        }

        [Client]
        private void DarklandUnitOnClientStarted() {
            slider.maxValue = _statsHolder.ValueOf(StatId.MaxHealth).Current;
            slider.value = _statsHolder.ValueOf(StatId.Health).Basic;
        }

        [Client]
        private void ClientOnStatsChanged(StatId statId, StatVal statVal) {
            if (statId == StatId.MaxHealth)
                slider.maxValue = statVal.Current;
            else if (statId == StatId.Health) slider.value = statVal.Basic;
        }

    }

}