using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class FireballSpellVfxManager : MonoBehaviour {

        [SerializeField]
        private GameObject fireBallVfxPrefab;

        private void Awake() {
            SpellMessagesProxy.ClientFireballVfxReceived += ClientFireballVfxReceived;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ClientFireballVfxReceived -= ClientFireballVfxReceived;
        }

        [Client]
        private void ClientFireballVfxReceived(SpellMessages.FireballSpellVfxResponseMessage message) =>
            Instantiate(fireBallVfxPrefab).GetComponent<IFireballSpellVfx>().BeginVfx(message);

    }

}