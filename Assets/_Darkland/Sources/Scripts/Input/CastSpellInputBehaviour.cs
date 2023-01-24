using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class CastSpellInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction spell0Action;

        private void OnEnable() {
            DarklandHero.LocalHeroStarted += Connect;
            DarklandHero.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHero.LocalHeroStarted -= Connect;
            DarklandHero.LocalHeroStopped -= Disconnect;
        }

        private void Connect() {
            spell0Action.Enable();
            spell0Action.performed += ClientSendSpellCastInput;
        }

        private void Disconnect() {
            spell0Action.Disable();
            spell0Action.performed -= ClientSendSpellCastInput;
        }

        [Client]
        private static void ClientSendSpellCastInput(InputAction.CallbackContext _) {
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage {spellIdx = 0});
        }

    }

}