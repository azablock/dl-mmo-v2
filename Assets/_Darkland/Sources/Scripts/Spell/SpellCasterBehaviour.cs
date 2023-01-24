using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.ScriptableObjects.Spell;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Spell {

    public interface ISpellCaster {
        void CastSpell(int spellIdx);
        event Action<SpellCastedEvent> ClientSpellCasted;
    }

    public struct SpellCastedEvent {

        public int spellIdx;
        public float cooldown;

    }

    public class SpellCasterBehaviour : NetworkBehaviour, ISpellCaster {

        [SerializeField]
        private List<SpellDef> spells;

        private readonly Dictionary<int, bool> _spellCooldowns = new();

        public event Action<SpellCastedEvent> ClientSpellCasted;

        public override void OnStartServer() {
            Assert.IsNotNull(spells);
            for (var i = 0; i < spells.Count; i++) _spellCooldowns.Add(i, true);
        }

        [Server]
        public void CastSpell(int spellIdx) {
            Assert.IsNotNull(spells);
            Assert.IsTrue(spellIdx >= 0 && spellIdx < spells.Count);

            var spell = spells[spellIdx];

            //network server send failure status
            if (!_spellCooldowns[spellIdx]) {
                // Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"Spell ({spellIdx}) not ready"));
                return; 
            }

            if (spell.InstantEffects.Any(it => !it.IsValid(gameObject))) {
                // Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"Spell ({spellIdx}) cannot be cast"));
                return;
            }

            spell.InstantEffects.ForEach(it => it.Process(gameObject));
            //apply timed effects

            //client rpc visualize used spell
            // Debug.LogWarning(ChatMessagesFormatter.FormatServerLog($"ServerUseSpell({spellIdx})"));

            TargetRpcNotifySpellCasted(new SpellCastedEvent{spellIdx = spellIdx, cooldown = spell.Cooldown()});
            StartCoroutine(ServerSpellCooldown(spellIdx));
        }

        [Server]
        private IEnumerator ServerSpellCooldown(int spellIdx) {
            _spellCooldowns[spellIdx] = false;
            yield return new WaitForSeconds(spells[spellIdx].Cooldown());
            _spellCooldowns[spellIdx] = true;
        }

        [TargetRpc]
        private void TargetRpcNotifySpellCasted(SpellCastedEvent evt) => ClientSpellCasted?.Invoke(evt);

    }

}