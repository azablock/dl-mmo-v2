using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.StatConstraint;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class StatPreChangeHooksBehaviour : MonoBehaviour, IStatPreChangeHooksHolder {

        [SerializeField]
        private List<StatPreChangeHookEntry> preChangeHooks;

        [Server]
        public IEnumerable<IStatPreChangeHook> PreChangeHooks(StatId id) {
            var hookEntries = preChangeHooks ?? new List<StatPreChangeHookEntry>();

            var hasHookWithStatId = hookEntries
                                                .Select(it => it.Id)
                                                .Contains(id);

            return hasHookWithStatId
                ? hookEntries
                  .First(it => it.Id == id)
                  .Hooks
                : new List<StatPreChangeHook>();
        }
    }

}