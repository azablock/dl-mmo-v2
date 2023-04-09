using System.Linq;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class SpellMessagesHandler : MonoBehaviour {

        private void Awake() {
            SpellMessagesProxy.ServerGetAvailableSpellsReceived += ServerHandleGetAvailableSpells;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ServerGetAvailableSpellsReceived -= ServerHandleGetAvailableSpells;
        }

        [Server]
        private static void ServerHandleGetAvailableSpells(NetworkConnectionToClient conn,
                                                           SpellMessages.GetAvailableSpellsRequestMessage message) {
            var spellNames = conn
                .identity
                .GetComponent<ISpellCaster>()
                .AvailableSpells
                .Select(it => it.Id)
                .ToList();

            conn.Send(new SpellMessages.GetAvailableSpellsResponseMessage { spellNames = spellNames });
        }

    }

}