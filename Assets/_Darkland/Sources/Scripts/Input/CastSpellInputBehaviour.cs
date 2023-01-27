using System.Collections.Generic;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class CastSpellInputBehaviour : MonoBehaviour {

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
            for (var i = 0; i < spellActions.Count; i++) {
                var action = spellActions[i];
                action.Enable();
                var idx = i;
                action.performed += _ => ClientSendSpellCastInput(idx);
            }
        }

        private void Disconnect() {
            for (var i = 0; i < spellActions.Count; i++) {
                var action = spellActions[i];
                action.Disable();
                var idx = i;
                action.performed -= _ => ClientSendSpellCastInput(idx);
            }
        }

        [Client]
        private static void ClientSendSpellCastInput(in int spellIdx) {
            if (InputStateBehaviour._.chatInputActive) return;
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage {spellIdx = spellIdx});
        }

    }

}