using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpHolder {
        void ChangeHp(int hpDelta);
        void ChangeMaxHp(int maxHpDelta);
        int hp { get; }
        int maxHp { get; }
        event Action<int> hpChanged;
        event Action<int> maxHpChanged;
    }

}