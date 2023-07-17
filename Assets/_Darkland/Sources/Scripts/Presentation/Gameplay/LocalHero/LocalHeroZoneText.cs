using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class LocalHeroZoneText : MonoBehaviour {

        [SerializeField]
        private TMP_Text localHeroZoneText;
        [SerializeField]
        private float visibilityDuration;

        [Client]
        public void ClientShow(string zoneName) => StartCoroutine(ClientShowText(zoneName));

        [Client]
        private IEnumerator ClientShowText(string zoneName) {
            localHeroZoneText.alpha = 0.0f;
            localHeroZoneText.text = $"{zoneName}";
            var t = 0.0f;

            while (t < 0.5f) {
                localHeroZoneText.alpha = Mathf.Lerp(0.0f, 1.0f, t);
                t += Time.deltaTime;
                yield return null;
            }
            
            while (t < 1.0f) {
                localHeroZoneText.alpha = Mathf.Lerp(1.0f, 0.0f, t);
                t += Time.deltaTime;
                yield return null;
            }
            
            localHeroZoneText.alpha = 0.0f;
        }

    }

}