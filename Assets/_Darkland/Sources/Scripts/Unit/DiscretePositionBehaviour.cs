using _Darkland.Sources.Models.Unit;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class DiscretePositionBehaviour : NetworkBehaviour {

        public DiscretePosition discretePosition { get; } = new DiscretePosition();
    }

}