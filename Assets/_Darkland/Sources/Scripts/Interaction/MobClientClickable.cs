using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Presentation.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Interaction {

    public class MobClientClickable : MonoBehaviour, IPointerClickHandler {

        [Client]
        public void OnPointerClick(PointerEventData _) {
            NetworkClient.Send(new PlayerInputMessages.NpcClickRequestMessage {npcNetId = GetComponent<DarklandUnit>().netId});
        }
    }

}