using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Spell;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class SpellsPanel : MonoBehaviour {

        [SerializeField]
        private List<GameObject> spellIconPrefabs;

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<ISpellCaster>().ClientSpellCasted += ClientOnSpellCasted;
            SpellMessagesProxy.ClientGetAvailableSpellsReceived += ClientInit;

            NetworkClient.Send(new SpellMessages.GetAvailableSpellsRequestMessage());
        }

        private void OnDisable() {
            spellIcons.ForEach(it => it.Clicked -= ClientOnSpellIconClicked);
            foreach (Transform child in transform) Destroy(child.gameObject);
            
            SpellMessagesProxy.ClientGetAvailableSpellsReceived -= ClientInit;
            DarklandHeroBehaviour.localHero.GetComponent<ISpellCaster>().ClientSpellCasted -= ClientOnSpellCasted;
        }

        [Client]
        private void ClientInit(SpellMessages.GetAvailableSpellsResponseMessage message) {
            message
                .spellNames
                .Select(it => Instantiate(ClientSpellIconPrefab(it), transform))
                .Select(it => it.GetComponent<SpellIconBehaviour>())
                .ToList()
                .ForEach(it => it.Clicked += ClientOnSpellIconClicked);

            for (var i = 0; i < spellIcons.Count; i++) spellIcons[i].ClientInit(i + 1);
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
            NetworkClient.Send(new PlayerInputMessages.CastSpellRequestMessage { spellIdx = idx });
        }

        [Client]
        private GameObject ClientSpellIconPrefab(string spellName) {
            var idx = spellIconPrefabs
                .FindIndex(it => it.GetComponent<SpellIconBehaviour>().SpellDef.Id.Equals(spellName));

            Assert.IsTrue(idx > -1 && idx < spellIconPrefabs.Count);

            return spellIconPrefabs[idx];
        }

        private List<SpellIconBehaviour> spellIcons => GetComponentsInChildren<SpellIconBehaviour>().ToList();

    }

}