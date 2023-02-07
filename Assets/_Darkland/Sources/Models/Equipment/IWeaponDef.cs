namespace _Darkland.Sources.Models.Equipment {

    public interface IWeaponDef {

        int MinDamage { get; }
        int MaxDamage { get; }
        int AttackRange { get; }

    }

}