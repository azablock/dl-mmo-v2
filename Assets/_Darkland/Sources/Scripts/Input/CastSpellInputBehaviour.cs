using System.Collections.Generic;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class CastSpellInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction spell0Action;
        // [SerializeField]
        // private InputAction spell1Action;
        [SerializeField]
        private List<InputAction> spellActions;

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
            
            for (var i = 0; i < spellActions.Count; i++) {
                var action = spellActions[i];
                action.Enable();
                action.performed += _ => ClientSendSpellCastInput2(i);
            }
        }

        private void Disconnect() {
            spell0Action.Disable();
            spell0Action.performed -= ClientSendSpellCastInput;
            
            for (var i = 0; i < spellActions.Count; i++) {
                var action = spellActions[i];
                action.Disable();
                action.performed -= _ => ClientSendSpellCastInput2(i);
            }
        }

        [Client]
        private static void ClientSendSpellCastInput(InputAction.CallbackContext _) {
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage {spellIdx = 0});
        }

        [Client]
        private static void ClientSendSpellCastInput2(int spellIdx) {
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage {spellIdx = spellIdx});
        }

    }

}