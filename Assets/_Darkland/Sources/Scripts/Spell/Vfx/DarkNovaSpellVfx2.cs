using System.Collections;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.NetworkMessages;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface IDarkNovaSpellVfx : ISpellVfx<SpellMessages.DarkNovaSpellVfxResponseMessage> { }

    public class DarkNovaSpellVfx2 : MonoBehaviour, IDarkNovaSpellVfx {

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Light2D light2D;
        [SerializeField]
        private Gradient novaGradient;
        [SerializeField]
        private float vfxSpeed;

        public void BeginVfx(SpellMessages.DarkNovaSpellVfxResponseMessage message) {
            StartCoroutine(Vfx(message));
        }

        private IEnumerator Vfx(SpellMessages.DarkNovaSpellVfxResponseMessage message) {
            var sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
            spriteRenderer.sortingLayerID = sortingLayerID;
            transform.position = message.castPos;
            Gfx2dHelper.ApplyLight2dSortingLayer(light2D, Vector3Int.FloorToInt(transform.position));


            var lerp = 0.0f;
            var initialOuterRadius = light2D.pointLightOuterRadius;

            while (lerp < 1.0f) {
                light2D.pointLightOuterRadius =
                    Mathf.Lerp(initialOuterRadius, initialOuterRadius + message.radius, lerp);
                light2D.color = novaGradient.Evaluate(lerp);
                lerp += Time.deltaTime * vfxSpeed;
                yield return null;
            }

            Destroy(gameObject);
        }

    }

}