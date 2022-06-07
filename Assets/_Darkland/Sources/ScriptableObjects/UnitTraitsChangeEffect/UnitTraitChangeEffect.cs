using _Darkland.Sources.Models.Unit.Effect;
using _Darkland.Sources.Models.Unit.Traits;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.UnitTraitsChangeEffect {

    [CreateAssetMenu(menuName = "Darkland/IUnitTraitsChangeEffect")]
    public class UnitTraitEffect : ScriptableObject, IUnitTraitsEffect {

        public UnitTraitId traitId;
        public int traitBonusDelta;

        public void OnEffectStart(IUnitTraitsHolder unitTraitsHolder) {
            unitTraitsHolder.ChangeBonus(traitId, traitBonusDelta);
        }

        public void OnEffectStop(IUnitTraitsHolder unitTraitsHolder) {
            unitTraitsHolder.ChangeBonus(traitId, -traitBonusDelta);
        }
    }

}