namespace _Darkland.Sources.Models.Unit.Death {

    //todo use/refactor
    public interface IUnitDeathHandlingStrategy {
        IUnitDeathEventEmitter UnitDeathEventEmitter { get; }
        void HandleUnitDeath();
    }

}