using System;
using System.Collections.Generic;
using _Darkland.Sources.Scripts.Spell;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class HotkeysPanel : MonoBehaviour {

        [SerializeField]
        private List<SpellIconBehaviour> spellIcons;

        private void OnEnable() {
            DarklandHero.localHero.GetComponent<ISpellCaster>().ClientSpellCasted += ClientOnSpellCasted;
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<ISpellCaster>().ClientSpellCasted -= ClientOnSpellCasted;
        }

        [Client]
        private void ClientOnSpellCasted(SpellCastedEvent evt) {
            var spellIdx = evt.spellIdx;
            Assert.IsTrue(spellIdx > -1 && spellIdx < spellIcons.Count);
            spellIcons[spellIdx].ClientStartCooldown(evt.cooldown);
        }

    }

}