using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.ScriptableObjects.Ai;
using _Darkland.Sources.Scripts.Presentation.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public class FiniteStateMachineBehaviour : MonoBehaviour {

        public float reactionRate = 1.0f;

        [SerializeField]
        private FiniteStateMachineBlueprint fsmBlueprint;
        private IFsmState _currentState;
        private DarklandUnit _darklandUnit;

        private void Awake() {
            Assert.IsNotNull(fsmBlueprint);
            Assert.IsNotNull(fsmBlueprint.initialState);

            _darklandUnit = GetComponent<DarklandUnit>();
        }

        private void OnEnable() {
            _darklandUnit.ServerStarted += OnServerUnitStarted;
            _darklandUnit.ServerStopped += OnServerUnitStopped;
        }

        private void OnDisable() {
            _darklandUnit.ServerStarted -= OnServerUnitStarted;
            _darklandUnit.ServerStopped -= OnServerUnitStopped;
        }

        private void OnServerUnitStarted() {
            _currentState = fsmBlueprint.initialState;
            InvokeRepeating(nameof(ServerFsmReact), 0.0f, reactionRate);
        }

        private void OnServerUnitStopped() => CancelInvoke(nameof(ServerFsmReact));

        [Server]
        private void ServerFsmReact() {
            Assert.IsNotNull(_currentState);

            var fsmTransition = _currentState
                .Transitions
                .FirstOrDefault(it => it.Decisions.All(d => d.Decide(gameObject)));

            if (fsmTransition != null) {
                _currentState.ExitSelf(gameObject);
                _currentState = fsmTransition.TargetState;
                _currentState.EnterSelf(gameObject);
            }

            Debug.Log($"_currentState\t{((FsmState)_currentState).name} {NetworkTime.time}");
            _currentState.UpdateSelf(gameObject);
        }

    }

}