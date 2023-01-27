using System.Collections.Generic;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Spell;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class HotkeysPanel : MonoBehaviour {

        [SerializeField]
        private List<SpellIconBehaviour> spellIcons;

        private void OnEnable() {
            Assert.IsNotNull(spellIcons);
            
            DarklandHero.localHero.GetComponent<ISpellCaster>().ClientSpellCasted += ClientOnSpellCasted;
            spellIcons.ForEach(it => it.Clicked += ClientOnSpellIconClicked);
        }

        private void OnDisable() {
            DarklandHero.localHero.GetComponent<ISpellCaster>().ClientSpellCasted -= ClientOnSpellCasted;
            spellIcons.ForEach(it => it.Clicked -= ClientOnSpellIconClicked);
        }

        [Client]
        private void ClientOnSpellCasted(SpellCastedEvent evt) {
            var spellIdx = evt.spellIdx;
            Assert.IsTrue(spellIdx > -1 && spellIdx < spellIcons.Count);
            spellIcons[spellIdx].ClientStartCooldown(evt.cooldown);
        }

        [Client]
        private void ClientOnSpellIconClicked(SpellIconBehaviour spellIcon) {
            var idx = spellIcons.IndexOf(spellIcon);
            Assert.IsTrue(idx > -1 && idx < spellIcons.Count);
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage {spellIdx = idx});
        }

    }

}