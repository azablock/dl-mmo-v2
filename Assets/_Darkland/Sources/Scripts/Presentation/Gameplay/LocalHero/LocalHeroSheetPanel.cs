using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class LocalHeroSheetPanel : MonoBehaviour {

        [Header("Traits UI")]
        [SerializeField]
        private TMP_Text mightText;
        [SerializeField]
        private TMP_Text constitutionText;

        private readonly Dictionary<StatId, Action<float, LocalHeroSheetPanel>> _statChangedCallbacks = new() {
            { StatId.Might, (val, panel) => panel.mightText.text = $"Might: {val}" },
            { StatId.Constitution, (val, panel) => panel.constitutionText.text = $"Constitution: {val}" },
        };

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged += OnClientChanged;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<IStatsHolder>().ClientChanged -= OnClientChanged;
        }

        [Client]
        private void OnClientChanged(StatId statId, float val) {
            if (!_statChangedCallbacks.ContainsKey(statId)) return;

            _statChangedCallbacks[statId].Invoke(val, this);
        }

    }

}