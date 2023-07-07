using System.Collections;
using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static _Darkland.Sources.NetworkMessages.SpellMessages;

namespace _Darkland.Sources.Scripts.Spell.Vfx {

    public interface ISpellVfx<in T> where T : NetworkMessage {

        void BeginVfx(T message);

    }

    public interface IFireballSpellVfx : ISpellVfx<FireballSpellVfxResponseMessage> { }

    public class FireballSpellVfx : MonoBehaviour, IFireballSpellVfx {

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Light2D light2D;

        private void Awake() { }

        [Client]
        public void BeginVfx(FireballSpellVfxResponseMessage message) {
            StartCoroutine(Vfx(message));
        }

        [Client]
        private IEnumerator Vfx(FireballSpellVfxResponseMessage message) {
            transform.position = message.castPos;
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
            Gfx2dHelper.ApplyLight2dSortingLayer(light2D, Vector3Int.FloorToInt(transform.position));

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