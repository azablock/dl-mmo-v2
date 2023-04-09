using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public class TransferManaSpellVfxManager : MonoBehaviour {

        [SerializeField]
        private GameObject transferManaVfxPrefab;

        private void Awake() {
            SpellMessagesProxy.ClientTransferManaVfxReceived += ClientTransferManaReceived;
        }

        private void OnDestroy() {
            SpellMessagesProxy.ClientTransferManaVfxReceived -= ClientTransferManaReceived;
        }

        private void ClientTransferManaReceived(SpellMessages.TransferManaSpellVfxResponseMessage message) {
            Instantiate(transferManaVfxPrefab).GetComponent<ITransferManaSpellVfx>().BeginVfx(message);
        }

    }

}