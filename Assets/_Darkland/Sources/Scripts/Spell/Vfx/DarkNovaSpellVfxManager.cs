using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class DarkNovaSpellVfxManager : MonoBehaviour {

        [SerializeField]
        private GameObject vfxPrefab;

        private void Awake() {
            SpellMessagesProxy.ClientDarkNovaVfxReceived += ClientDarkNovaReceived;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ClientDarkNovaVfxReceived -= ClientDarkNovaReceived;
        }

        private void ClientDarkNovaReceived(SpellMessages.DarkNovaSpellVfxResponseMessage message) {
            Instantiate(vfxPrefab).GetComponent<IDarkNovaSpellVfx>().BeginVfx(message);
        }

    }

}