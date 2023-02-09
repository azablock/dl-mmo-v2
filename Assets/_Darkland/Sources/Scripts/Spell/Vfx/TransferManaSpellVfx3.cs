using System.Collections;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface ITransferManaSpellVfx : ISpellVfx<SpellMessages.TransferManaSpellVfxResponseMessage> {}
    
    public class TransferManaSpellVfx3 : MonoBehaviour, ITransferManaSpellVfx {

        [SerializeField]
        private GameObject transferManaMarkerPrefab;
        [SerializeField]
        private float vfxDuration;

        [Client]
        public void BeginVfx(SpellMessages.TransferManaSpellVfxResponseMessage message) => StartCoroutine(Vfx(message));

        [Client]
        private IEnumerator Vfx(SpellMessages.TransferManaSpellVfxResponseMessage message) {
            var manaLoseMarker = Instantiate(transferManaMarkerPrefab, message.castPos, Quaternion.identity);
            var manaGainMarker = Instantiate(transferManaMarkerPrefab, message.targetPos, Quaternion.identity);

            var lerp = 0.0f;
            
            while (lerp < 1.0f) {
                manaLoseMarker.transform.position = 
                    Vector3.Lerp(message.castPos, b: message.castPos - Vector3.up * 0.5f , lerp);

                manaGainMarker.transform.position = 
                    Vector3.Lerp(message.targetPos, b: message.targetPos + (Vector3.up * 1.5f) , lerp);
                
                lerp += Time.deltaTime / vfxDuration;

                yield return null;
            }

            Destroy(manaLoseMarker);
            Destroy(manaGainMarker);
            Destroy(gameObject);
        }

    }

}