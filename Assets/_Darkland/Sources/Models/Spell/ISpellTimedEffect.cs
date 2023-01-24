using System.Collections;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellTimedEffect {
        [Server]
        void PreProcess(GameObject caster);
        [Server]
        void PostProcess(GameObject caster);
        [Server]
        bool IsValid(GameObject caster);
        [Server]
        IEnumerator Process(GameObject caster);
    }

}