using System;
using _Darkland.Sources.Scripts.Unit;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitConditionEffectHolder {

        void Add(IUnitConditionEffect effect);
        void Remove(IUnitConditionEffect effect);

        event Action<IUnitConditionEffect> ServerEffectAdded;
        event Action<IUnitConditionEffect> ServerEffectRemoved;

        event Action<string> ClientEffectAdded;
        event Action<string> ClientEffectRemoved;

    }

}