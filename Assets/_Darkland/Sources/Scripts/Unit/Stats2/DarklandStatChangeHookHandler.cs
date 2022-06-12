using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class DarklandStatChangeHookHandler : NetworkBehaviour {
    
        public DarklandStatChangeHook[] statChangeHooks;

        private IDarklandStatsHolder _darklandStatsHolder;

        private void Awake() {
            _darklandStatsHolder = GetComponent<IDarklandStatsHolder>();
        }

        public override void OnStartServer() {
            //todo gdzies indziej ten check
            Debug.Assert(statChangeHooks.All(it => it.CanBeRegistered(_darklandStatsHolder)));
            
            foreach (var darklandStatChangeHook in statChangeHooks) {
                darklandStatChangeHook.Register(_darklandStatsHolder);
            }
        }

    }

}