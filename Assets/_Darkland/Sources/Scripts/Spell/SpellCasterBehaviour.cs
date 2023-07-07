using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Spell {

    public struct SpellCastedEvent {

        public int spellIdx;
        public float cooldown;
        public NetworkIdentity casterIdentity;

    }

    public class SpellCasterBehaviour : NetworkBehaviour, ISpellCaster {

        private readonly Dictionary<int, bool> _spellCooldowns = new();
        private DarklandHeroBehaviour _darklandHeroBehaviour;
        private IStatsHolder _statsHolder;

        private List<ISpell> availableSpells => _darklandHeroBehaviour.heroVocation.AvailableSpells;

        private void Awake() {
            _darklandHeroBehaviour = GetComponent<DarklandHeroBehaviour>();
            _statsHolder = GetComponent<IStatsHolder>();
        }

        public List<ISpell> AvailableSpells => availableSpells;
        public event Action<SpellCastedEvent> ClientSpellCasted;

        [Server]
        public void CastSpell(int spellIdx) {
            Assert.IsNotNull(availableSpells);

            // Assert.IsTrue(spellIdx >= 0 && spellIdx < spells.Count);
            if (spellIdx >= availableSpells.Count) {
                // Debug.LogWarning(RichTextFormatter.FormatServerLog($"Spell ({spellIdx}) is empty"));

                netIdentity.connectionToClient.Send(new ChatMessages.ServerLogResponseMessage {
                    message = RichTextFormatter.FormatServerLog($"Spell ({spellIdx}) is empty")
                });

                return;
            }

            var spell = availableSpells[spellIdx];

            if (!_spellCooldowns[spellIdx]) {
                netIdentity.connectionToClient.Send(new ChatMessages.ServerLogResponseMessage {
                    message = RichTextFormatter.FormatServerLog($"Spell ({spellIdx}) not ready.")
                });

                // Debug.LogWarning(RichTextFormatter.FormatServerLog($"Spell ({spellIdx}) not ready"));
                return;
            }

            var invalidCastCondition = spell
                .CastConditions
                .FirstOrDefault(it => !it.CanCast(gameObject, spell));

            if (invalidCastCondition != null) {
                netIdentity.connectionToClient.Send(new ChatMessages.ServerLogResponseMessage {
                    message = RichTextFormatter.FormatServerLog(invalidCastCondition.InvalidCastMessage())
                });

                // Debug.LogWarning(RichTextFormatter.FormatServerLog(invalidCastCondition.InvalidCastMessage()));
                return;
            }

            //todo gdzie to w sumie umiescic? pomysl 1: InstantEffects - dodac caster subtract mana effect
            _statsHolder.Subtract(StatId.Mana, StatVal.OfBasic(spell.ManaCost));

            spell.InstantEffects.ForEach(it => it.Process(gameObject));
            spell.TimedEffects.ForEach(it => StartCoroutine(it.Process(gameObject)));

            var cooldown = spell.Cooldown(gameObject);

            TargetRpcSpellCasted(new SpellCastedEvent {
                spellIdx = spellIdx,
                cooldown = cooldown,
                casterIdentity = netIdentity
            });

            StartCoroutine(ServerSpellCooldown(spellIdx, cooldown));
        }

        public override void OnStartServer() {
            for (var i = 0; i < availableSpells.Count; i++) _spellCooldowns.Add(i, true);
        }

        public override void OnStopServer() {
            _spellCooldowns.Clear();
        }

        [Server]
        private IEnumerator ServerSpellCooldown(int spellIdx, float cooldown) {
            _spellCooldowns[spellIdx] = false;
            yield return new WaitForSeconds(cooldown);
            _spellCooldowns[spellIdx] = true;
        }

        [TargetRpc]
        private void TargetRpcSpellCasted(SpellCastedEvent evt) {
            ClientSpellCasted?.Invoke(evt);
        }

    }

}