using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpHolder {
        int hp { get; }
        int maxHp { get; }
        event Action<int> HpChanged;
        event Action<int> MaxHpChanged;

        void ChangeHp(int hpDelta); //nie jestem pewien czy to jest dobrze ze te 2 metody doszly

        void ChangeMaxHp(int maxHpDelta);
    }
}