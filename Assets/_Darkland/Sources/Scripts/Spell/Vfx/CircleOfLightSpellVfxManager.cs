using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class CircleOfLightSpellVfxManager : MonoBehaviour {

        [SerializeField]
        private GameObject vfxPrefab;

        private void Awake() {
            SpellMessagesProxy.ClientCircleOfLightVfxReceived += ClientCircleOfLightReceived;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ClientCircleOfLightVfxReceived -= ClientCircleOfLightReceived;
        }

        private void ClientCircleOfLightReceived(SpellMessages.CircleOfLightSpellVfxResponseMessage message) {
            Instantiate(vfxPrefab).GetComponent<ICircleOfLightSpellVfx>().BeginVfx(message);
        }

    }

}