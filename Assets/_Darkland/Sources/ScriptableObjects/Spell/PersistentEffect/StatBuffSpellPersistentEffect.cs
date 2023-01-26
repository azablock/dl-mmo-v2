using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.PersistentEffect {

    public class StatBuffSpellPersistentEffect : SpellPersistentEffect {

        public StatId statId;
        public float buffValue;
        
        public override void Activate(GameObject caster) {
            //todo to jest zle - bo np. wartosc Might jest persystentna, wiec float -> StatValue {float basic, float bonus}
            caster
                .GetComponent<IStatsHolder>()
                .Stat(statId)
                .Add(buffValue);
        }

        public override void Deactivate(GameObject caster) {
            caster
                .GetComponent<IStatsHolder>()
                .Stat(statId)
                .Add(-buffValue);
        }

    }

}