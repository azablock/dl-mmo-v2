using _Darkland.Sources.Models.Unit.DiscretePosition;

namespace _Darkland.Sources.Models.Property {

    public interface ILimitedRangeProperty {
        IDiscretePosition FromPosition { get; }
        IDiscretePosition ToPosition { get; }
        int MaxRange { get; }
    }

}