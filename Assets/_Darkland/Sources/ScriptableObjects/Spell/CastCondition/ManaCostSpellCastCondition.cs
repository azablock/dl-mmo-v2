using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(ManaCostSpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(ManaCostSpellCastCondition))]
    public class ManaCostSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell spell) {
            return caster.GetComponent<IStatsHolder>().ValueOf(StatId.Mana).Basic >= spell.ManaCost;
        }

        public override string InvalidCastMessage() => "Not enough mana";

    }

}