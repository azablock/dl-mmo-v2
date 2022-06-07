using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Traits;

namespace _Darkland.Sources.Models.Unit.Effect {

    public interface IUnitTraitsEffect {
        void OnEffectStart(IUnitTraitsHolder unitTraitsHolder);
        void OnEffectStop(IUnitTraitsHolder unitTraitsHolder);
    }

    public interface IUnitTraitsEffectsHolder {
        List<IUnitTraitsEffect> ActiveEffects { get; }
        IUnitTraitsHolder UnitTraitsHolder { get; }

        void Add(IUnitTraitsEffect effect);
        void Remove(IUnitTraitsEffect effect);
    }

    public class UnitTraitsEffectsHolder : IUnitTraitsEffectsHolder {

        public List<IUnitTraitsEffect> ActiveEffects { get; }
        public IUnitTraitsHolder UnitTraitsHolder { get; }

        public UnitTraitsEffectsHolder(IUnitTraitsHolder unitTraitsHolder) {
            UnitTraitsHolder = unitTraitsHolder;
            ActiveEffects = new List<IUnitTraitsEffect>();
        }

        public void Add(IUnitTraitsEffect effect) {
            ActiveEffects.Add(effect);
            effect.OnEffectStart(UnitTraitsHolder);
        }

        public void Remove(IUnitTraitsEffect effect) {
            ActiveEffects.Remove(effect);
            effect.OnEffectStop(UnitTraitsHolder);
        }
    }

}