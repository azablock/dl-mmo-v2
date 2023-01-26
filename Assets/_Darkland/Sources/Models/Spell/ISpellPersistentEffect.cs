using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellPersistentEffect {

        void Activate(GameObject caster);
        void Deactivate(GameObject caster);

    }

}