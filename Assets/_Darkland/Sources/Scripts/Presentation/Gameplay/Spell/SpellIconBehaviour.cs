using System;
using System.Collections;
using _Darkland.Sources.ScriptableObjects.Spell;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class SpellIconBehaviour : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private Image cooldownImage;
        [SerializeField]
        private TMP_Text cooldownText;
        [SerializeField]
        private TMP_Text hotkeyText;
        [SerializeField]
        private float cooldownDelta = 0.1f;
        [SerializeField]
        private SpellDef spellDef;

        private Coroutine _cooldownCoroutine;

        public event Action<SpellIconBehaviour> Clicked;

        private void OnEnable() {
            Assert.IsNotNull(spellDef);
        }

        private void OnDisable() {
            cooldownImage.gameObject.SetActive(false);
        }

        public void OnPointerClick(PointerEventData _) => Clicked?.Invoke(this);

        [Client]
        public void ClientInit(in int idx) => hotkeyText.text = $"{idx}";
        
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
        
        public SpellDef SpellDef => spellDef;

    }

}