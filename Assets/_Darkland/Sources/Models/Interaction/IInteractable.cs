using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.World;
using _Darkland.Sources.Scripts.Movement;
using Mirror;

namespace _Darkland.Sources.Models.Interaction {

    public interface IInteractable<in T> {
        [Server]
        void Interact(NetworkIdentity interactor, T target);

        [Server]
        bool CanInteract(NetworkIdentity interactor, T target);
    }

    public class TeleportTileInteractable : IInteractable<IDarklandTeleportTile> {

        [Server]
        public void Interact(NetworkIdentity interactor, IDarklandTeleportTile target) {
            var interactorDiscretePosition = interactor.GetComponent<MovementBehaviour>();
            interactorDiscretePosition.ServerMoveOnceClientImmediate(target.posDelta);
        }

        [Server]
        public bool CanInteract(NetworkIdentity interactor, IDarklandTeleportTile target) {
            return interactor.GetComponent<IDiscretePosition>().Pos.Equals(target.position);
        }
    }

    public static class Interactions {

        public static readonly TeleportTileInteractable teleportTileInteractable = new();
    }

}