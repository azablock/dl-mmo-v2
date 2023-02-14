using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit {

    public interface IUnitEffectClientNotifier {

        event Action<string> ClientAdded;
        event Action<string> ClientRemoved;
        event Action ClientRemovedAll;
        event Action<List<string>> ClientNotified;
        List<string> ActiveEffectsNames { get; }

    }

}