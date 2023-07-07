using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class StatPostChangeHooksBehaviour : NetworkBehaviour {

        [SerializeField]
        private StatPostChangeHook[] statPostChangeHooks;
        private StatPostChangeHooksHandler _hooksHandler;
        private IStatsHolder _statsHolder;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _hooksHandler = new StatPostChangeHooksHandler(_statsHolder, statPostChangeHooks);

            Debug.Assert(statPostChangeHooks.All(it => it.CanBeRegistered(_statsHolder)));
        }

        public override void OnStartServer() {
            _hooksHandler.Register();
        }

        public override void OnStopServer() {
            _hooksHandler.Unregister();
        }

    }

}