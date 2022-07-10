using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class StatChangeHooksHandler : NetworkBehaviour {
    
        public StatChangeHook[] statChangeHooks;

        private IStatsHolder _statsHolder;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            Debug.Assert(statChangeHooks.All(it => it.CanBeRegistered(_statsHolder)));
        }

        public override void OnStartServer() {
            foreach (var hook in statChangeHooks) {
                hook.Register(_statsHolder);
            }
        }

        public override void OnStopServer() {
            foreach (var hook in statChangeHooks) {
                hook.Unregister(_statsHolder);
            }
        }

    }

}