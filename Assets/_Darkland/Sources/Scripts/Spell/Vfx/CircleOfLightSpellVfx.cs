using System.Collections;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.NetworkMessages;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface ICircleOfLightSpellVfx : ISpellVfx<SpellMessages.CircleOfLightSpellVfxResponseMessage> { }

    public class CircleOfLightSpellVfx : MonoBehaviour, ICircleOfLightSpellVfx {

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Light2D light2D;

        public void BeginVfx(SpellMessages.CircleOfLightSpellVfxResponseMessage message) {
            StartCoroutine(Vfx(message));
        }

        private IEnumerator Vfx(SpellMessages.CircleOfLightSpellVfxResponseMessage message) {
            var sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
            spriteRenderer.sortingLayerID = sortingLayerID;
            transform.position = message.castPos;
            Gfx2dHelper.ApplyLight2dSortingLayer(light2D, Vector3Int.FloorToInt(transform.position));

            transform.localScale = new Vector3(message.radius, message.radius, 1);
            var startOuterLightRadius = light2D.pointLightOuterRadius;

            var lerp = 0.0f;

            while (lerp < 1.0f) {
                light2D.pointLightOuterRadius = Mathf.Lerp(startOuterLightRadius, startOuterLightRadius + 4, lerp);
                lerp += Time.deltaTime / message.duration;
                yield return null;
            }

            Destroy(gameObject);
        }

    }

}