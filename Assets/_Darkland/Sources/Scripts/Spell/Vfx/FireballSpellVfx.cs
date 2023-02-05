using System.Collections;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.SpellMessages;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface ISpellVfx<in T> where T : NetworkMessage {

        void BeginVfx(T message);

    }
    
    public interface IFireballSpellVfx : ISpellVfx<FireballSpellVfxResponseMessage> {}

    public class FireballSpellVfx : MonoBehaviour, IFireballSpellVfx {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        [Client]
        public void BeginVfx(FireballSpellVfxResponseMessage message) => StartCoroutine(Vfx(message));

        [Client]
        private IEnumerator Vfx(FireballSpellVfxResponseMessage message) {
            transform.position = message.castPos;

            var lerp = 0.0f;
            var vfxDuration = message.fireballFlyDuration;
            
            while (!transform.position.Equals(message.targetPos)) {
                transform.position = Vector3.Lerp(message.castPos, message.targetPos, lerp);
                lerp += Time.deltaTime / vfxDuration;

                yield return null;
            }

            Destroy(gameObject);
        }

    }

}