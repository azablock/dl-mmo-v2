using System;
using System.Collections.Generic;
using _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    //todo probably change to SO
    [Serializable]
    public struct StatPreChangeHookEntry {
        [SerializeField]
        private StatId id;
        [SerializeField]
        private List<StatPreChangeHook> hooks;

        public readonly List<StatPreChangeHook> Hooks => hooks;
        public readonly StatId Id => id;
    }

}