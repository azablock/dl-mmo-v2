using _Darkland.Sources.Models.Unit.Hp;

namespace _Darkland.Sources.Models.Property {

    //todo maybe treat it not as ability interface, but "operation" interface (i.e. weapon user deals damage, monster deals damage etc.)
    public interface IDirectDamageProperty {
        int Damage { get; }
        IHpHolder TargetHpHolder { get; }
    }

}