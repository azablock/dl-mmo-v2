using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    [CreateAssetMenu(fileName = nameof(FireballSpell),
                     menuName = "DL/"  + nameof(SpellDef) + "/" + nameof(FireballSpell))]
    public class FireballSpell : SpellDef {

        public override string Description() {
            return "Fireball Description";
        }
        
        
    }

}