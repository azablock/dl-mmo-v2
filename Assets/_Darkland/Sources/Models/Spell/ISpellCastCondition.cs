using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellCastCondition {

        bool CanCast(GameObject caster);
        string InvalidCastMessage();

    }

}