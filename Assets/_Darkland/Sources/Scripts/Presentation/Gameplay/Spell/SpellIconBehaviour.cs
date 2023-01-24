using System;
using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class SpellIconBehaviour : MonoBehaviour {

        [SerializeField]
        private Image cooldownImage;
        [SerializeField]
        private TMP_Text cooldownText;
        [SerializeField]
        private float cooldownDelta = 0.1f;

        private Coroutine _cooldownCoroutine;

        private void OnDisable() {
            cooldownImage.gameObject.SetActive(false);
        }

        [Client]
        public void ClientStartCooldown(float cooldown) {
            if (_cooldownCoroutine != null) StopCoroutine(_cooldownCoroutine);
            _cooldownCoroutine = StartCoroutine(ClientCooldown(cooldown));
        }

        [Client]
        private IEnumerator ClientCooldown(float cooldown) {
            Assert.IsTrue(cooldown >= 0.0f);
            
            var remainingCooldown = cooldown;
            
            cooldownImage.gameObject.SetActive(true);

            while (remainingCooldown >= 0.0f) {
                cooldownText.text = $"{remainingCooldown:0.0}";
                yield return new WaitForSeconds(cooldownDelta);
                remainingCooldown -= cooldownDelta;
            }
            
            cooldownImage.gameObject.SetActive(false);
        }

    }

}