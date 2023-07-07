using _Darkland.Sources.Models.Core;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(Consumable),
                     menuName = "DL/Eq/" + nameof(Consumable) + "/" + nameof(ScrollOfEscapeConsumable))]
    public class ScrollOfEscapeConsumable : Consumable {

        public Vector3Int teleportPos;
        public string teleportPlaceName;

        public override void Consume(GameObject eqHolder) {
            eqHolder.GetComponent<IDiscretePosition>().Set(teleportPos, true);
            eqHolder.GetComponent<NetworkIdentity>()
                .connectionToClient.Send(new ChatMessages.ServerLogResponseMessage {
                    message = $"You've used the scroll and teleported to {teleportPlaceName}."
                });
        }

        public override string Description(GameObject parent) {
            return $"Teleports immediately to {teleportPlaceName}.";
        }

    }

}