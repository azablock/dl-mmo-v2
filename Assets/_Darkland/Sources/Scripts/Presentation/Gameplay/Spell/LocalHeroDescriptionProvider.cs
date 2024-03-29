using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Spell {

    public class LocalHeroDescriptionProvider : MonoBehaviour, IDescriptionProvider {

        private SpellIconBehaviour _spellIconBehaviour;

        private void Awake() {
            _spellIconBehaviour = GetComponent<SpellIconBehaviour>();
        }

        public TooltipDescription Get() {
            return new() {
                title = _spellIconBehaviour.SpellDef.SpellName,
                content = _spellIconBehaviour.SpellDef.Description(DarklandHeroBehaviour.localHero.gameObject)
            };
        }

    }

}