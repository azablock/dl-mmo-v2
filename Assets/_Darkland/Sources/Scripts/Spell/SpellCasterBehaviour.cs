using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Spell;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Spell {

    public struct SpellCastedEvent {

        public int spellIdx;
        public float cooldown;

    }

    public class SpellCasterBehaviour : NetworkBehaviour, ISpellCaster {

        private readonly Dictionary<int, bool> _spellCooldowns = new();
        private DarklandHero _darklandHero;

        public List<ISpell> AvailableSpells => availableSpells;
        public event Action<SpellCastedEvent> ClientSpellCasted;

        private void Awake() {
            _darklandHero = GetComponent<DarklandHero>();
        }

        public override void OnStartServer() {
            for (var i = 0; i < availableSpells.Count; i++) _spellCooldowns.Add(i, true);
        }

        public override void OnStopServer() {
            _spellCooldowns.Clear();
        }

        [Server]
        public void CastSpell(int spellIdx) {
            Assert.IsNotNull(availableSpells);

            // Assert.IsTrue(spellIdx >= 0 && spellIdx < spells.Count);
            if (spellIdx >= availableSpells.Count) {
                Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"Spell ({spellIdx}) is empty"));
                return;
            }
            
            var spell = availableSpells[spellIdx];

            if (!_spellCooldowns[spellIdx]) {
                Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"Spell ({spellIdx}) not ready"));
                return; 
            }

            var invalidCastCondition = spell
                .CastConditions
                .FirstOrDefault(it => !it.CanCast(gameObject));

            if (invalidCastCondition != null) {
                Debug.LogWarning(ChatMessagesFormatter.FormatServerLog(invalidCastCondition.InvalidCastMessage()));
                return;
            }

            spell.InstantEffects.ForEach(it => it.Process(gameObject));
            
            var cooldown = spell.Cooldown(gameObject);
            TargetRpcSpellCasted(new SpellCastedEvent{spellIdx = spellIdx, cooldown = cooldown});
            StartCoroutine(ServerSpellCooldown(spellIdx, cooldown));
        }

        [Server]
        private IEnumerator ServerSpellCooldown(int spellIdx, float cooldown) {
            _spellCooldowns[spellIdx] = false;
            yield return new WaitForSeconds(cooldown);
            _spellCooldowns[spellIdx] = true;
        }

        [TargetRpc]
        private void TargetRpcSpellCasted(SpellCastedEvent evt) => ClientSpellCasted?.Invoke(evt);

        private List<ISpell> availableSpells => _darklandHero.heroVocation.AvailableSpells;

    }

}