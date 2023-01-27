using System;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitEffectClientNotifier {

        event Action<string> ClientAdded;
        event Action<string> ClientRemoved;
        event Action ClientRemovedAll;

    }

}