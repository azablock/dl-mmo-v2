using System.Collections;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.NetworkMessages;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface IHealSpellVfx : ISpellVfx<SpellMessages.HealSpellVfxResponseMessage> { }

    public class HealSpellVfx : MonoBehaviour, IHealSpellVfx {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        public void BeginVfx(SpellMessages.HealSpellVfxResponseMessage message) {
            StartCoroutine(Vfx(message));
        }

        private IEnumerator Vfx(SpellMessages.HealSpellVfxResponseMessage message) {
            transform.position = message.targetPos;

            var lerp = 0.0f;

            while (lerp < 1.0f) {
                transform.position = Vector3.Lerp(message.targetPos, message.targetPos + Vector3.up, lerp);
                lerp += Time.deltaTime;
                yield return null;
            }

            Destroy(gameObject);
        }

    }

}