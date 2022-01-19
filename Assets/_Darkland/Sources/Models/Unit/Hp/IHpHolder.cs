using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpHolder {
        int hp { get; }
        int maxHp { get; }
        event Action<int> HpChanged;
        event Action<int> MaxHpChanged;
    }
}