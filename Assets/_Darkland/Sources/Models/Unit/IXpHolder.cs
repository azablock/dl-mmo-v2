using System;

namespace _Darkland.Sources.Models.Unit {

    public interface IXpHolder {
        int xp { get; }
        int level { get; }
        event Action<int> ServerLevelChanged;
        event Action<int> ClientXpChanged;
        event Action<ExperienceLevelChangeEvent> ClientLevelChanged;
    }

}