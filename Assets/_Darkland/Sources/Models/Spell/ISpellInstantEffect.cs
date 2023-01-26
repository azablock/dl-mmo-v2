using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellInstantEffect {
        [Server]
        void Process(GameObject caster);
    }

}