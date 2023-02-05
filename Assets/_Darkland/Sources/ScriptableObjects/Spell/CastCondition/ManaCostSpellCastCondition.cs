using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(ManaCostSpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(ManaCostSpellCastCondition))]
    public class ManaCostSpellCastCondition : SpellCastCondition {

        public float manaCost;

        public override bool CanCast(GameObject caster) {
            return caster.GetComponent<IStatsHolder>().ValueOf(StatId.Mana).Current > manaCost;
        }

        public override string InvalidCastMessage() => "Not enough mana";

    }

}