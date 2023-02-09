using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class HealSpellVfxManager : MonoBehaviour {

        [SerializeField]
        private GameObject vfxPrefab;

        private void Awake() {
            SpellMessagesProxy.ClientHealVfxReceived += ClientHealReceived;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ClientHealVfxReceived -= ClientHealReceived;
        }

        private void ClientCircleOfLightReceived(SpellMessages.CircleOfLightSpellVfxResponseMessage message) {
            Instantiate(vfxPrefab).GetComponent<ICircleOfLightSpellVfx>().BeginVfx(message);
        }

        private void ClientHealReceived(SpellMessages.HealSpellVfxResponseMessage message) {
            Instantiate(vfxPrefab).GetComponent<IHealSpellVfx>().BeginVfx(message);
        }

    }

}