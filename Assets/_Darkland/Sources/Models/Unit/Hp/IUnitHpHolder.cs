using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IUnitHpHolder {
        int hp { get; }
        int maxHp { get; }
        UnitHpActions unitHpActions { get; }
    }

}