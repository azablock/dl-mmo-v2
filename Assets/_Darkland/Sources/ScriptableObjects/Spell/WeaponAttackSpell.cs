using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    [CreateAssetMenu(fileName = nameof(WeaponAttackSpell),
                     menuName = "DL/"  + nameof(SpellDef) + "/" + nameof(WeaponAttackSpell))]
    public class WeaponAttackSpell : SpellDef {

        //todo weapon cooldown
        public override float Cooldown(GameObject caster) => 1;

        public override string Description() {
            return "Weapon Attack";
        }

    }

}