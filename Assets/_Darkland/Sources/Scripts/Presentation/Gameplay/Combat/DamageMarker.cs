using System.Collections;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Core;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Combat {

    public class DamageMarker : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer damageMarkerSpriteRenderer;
        [SerializeField]
        private Canvas damageMarkerCanvas;
        [SerializeField]
        private TMP_Text damageText;
        [SerializeField]
        private float fadeDuration = 1.0f;
        [SerializeField]
        private float damageMarkerPosOffset;
        [SerializeField]
        private Color physicalDamageColor;
        [SerializeField]
        private Color magicDamageColor;

        private void Awake() {
            var sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
            damageMarkerSpriteRenderer.sortingLayerID = sortingLayerID;
            damageMarkerCanvas.sortingLayerID = sortingLayerID;
        }

        [Client]
        public void ClientInit(int damage, DamageType damageType) {
            damageText.text = $"{damage}";
            damageText.color = damageType == DamageType.Physical ? physicalDamageColor : magicDamageColor;

            var markerRandomOffsetX = Random.Range(-damageMarkerPosOffset, damageMarkerPosOffset);
            var markerRandomOffsetY = Random.Range(-damageMarkerPosOffset, damageMarkerPosOffset);
            damageMarkerSpriteRenderer.transform.position += new Vector3(markerRandomOffsetX, markerRandomOffsetY, 0);

            StartCoroutine(ClientFadeDamageMarker());
        }

        [Client]
        private IEnumerator ClientFadeDamageMarker() {
            var fade = 0.0f;
            var textStartPosition = damageText.rectTransform.transform.position + new Vector3(0, 0.5f, 0);
            var textFinalPosition = textStartPosition + Vector3.up;

            while (fade < 1.0f) {
                damageText.rectTransform.position = Vector3.Lerp(textStartPosition, textFinalPosition, fade);
                damageText.alpha = 1.0f - fade / 2.0f;
                fade += Time.deltaTime / fadeDuration;

                if (fade > 0.5f) damageMarkerSpriteRenderer.gameObject.SetActive(false);

                yield return null;
            }

            Destroy(gameObject);
        }

    }

}