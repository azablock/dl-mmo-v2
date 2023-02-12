using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class TargetNetIdInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction currentTargetClear;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
        }
        
        private void Connect() {
            currentTargetClear.performed += ClientOnCurrentTargetCleared;
            currentTargetClear.Enable();
        }

        private void Disconnect() {
            currentTargetClear.performed -= ClientOnCurrentTargetCleared;
            currentTargetClear.Disable();
        }

        [Client]
        private static void ClientOnCurrentTargetCleared(InputAction.CallbackContext _) {
            NetworkClient.Send(new PlayerInputMessages.NpcClearRequestMessage());
        }

    }

}