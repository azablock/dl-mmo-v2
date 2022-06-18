using System;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class PlayerStatsDebugPanel : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI healthText;

        private void Awake() {
            DarklandPlayer.LocalPlayerStarted += OnLocalPlayerStarted;
            DarklandPlayer.LocalPlayerStopped += OnLocalPlayerStopped;
        }

        [Client]
        private void OnLocalPlayerStarted() {
            DarklandPlayer.localPlayer.StatsHolder.Stat(StatId.Health).Changed += OnHealthChanged;
        }

        [Client]
        private void OnLocalPlayerStopped() {
            DarklandPlayer.LocalPlayerStarted -= OnLocalPlayerStarted;
            DarklandPlayer.LocalPlayerStopped -= OnLocalPlayerStopped;
            DarklandPlayer.localPlayer.StatsHolder.Stat(StatId.Health).Changed -= OnHealthChanged;
        }

        [Client]
        private void OnHealthChanged(StatValue _) {
            var (healthStat, maxHealthStat) = DarklandPlayer.localPlayer.StatsHolder.Stats(StatId.Health, StatId.MaxHealth);
            healthText.text = $"Health (curr/max): {healthStat.Basic} / {maxHealthStat.Basic}";
        }
    }

}