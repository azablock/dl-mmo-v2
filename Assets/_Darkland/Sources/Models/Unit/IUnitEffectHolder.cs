using System;
using System.Collections.Generic;
using _Darkland.Sources.Scripts.Unit;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitEffectHolder {

        void ServerAdd(IUnitEffect effect);
        void ServerRemove(IUnitEffect effect);
        void ServerRemoveAll();

        event Action<IUnitEffect> ServerAdded;
        event Action<string> ServerRemoved;
        event Action ServerRemovedAll;

    }

}