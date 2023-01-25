using System.Collections;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Combat {

    public class DamageMarker : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private Canvas damageMarkerCanvas;
        [SerializeField]
        private TMP_Text damageText;
        [SerializeField]
        private float fadeDuration = 1.0f;

        private void Awake() {
            var sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
            spriteRenderer.sortingLayerID = sortingLayerID;
            damageMarkerCanvas.sortingLayerID = sortingLayerID;
        }

        [Client]
        public void ClientInit(int damage) {
            damageText.text = $"{damage}";
            StartCoroutine(ClientFadeDamageMarker());
        }

        [Client]
        private IEnumerator ClientFadeDamageMarker() {
            var fade = 0.0f;
            var textStartPosition = damageText.rectTransform.transform.position;
            var textFinalPosition = textStartPosition + Vector3.up;

            while (fade < 1.0f) {
                damageText.rectTransform.position = Vector3.Lerp(textStartPosition, textFinalPosition, fade);
                damageText.alpha = 1.0f - fade;
                fade += Time.deltaTime / fadeDuration;

                if (fade > 0.5f) spriteRenderer.gameObject.SetActive(false);
                
                yield return null;
            }

            Destroy(gameObject);
        }

    }

}