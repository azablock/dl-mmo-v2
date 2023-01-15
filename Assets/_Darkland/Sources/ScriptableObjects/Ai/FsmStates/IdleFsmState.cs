using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {
    
    [CreateAssetMenu(fileName = nameof(IdleFsmState), menuName = "DL/Ai/" + nameof(IdleFsmState))]
    public class IdleFsmState : FsmState {

        public override void UpdateSelf(GameObject parent) {
            var netId = parent.GetComponent<NetworkIdentity>().netId;
            var message = $"[netId]={netId} IdleFsmState UpdateSelf";
            
            // NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage {
                // message = ChatMessagesFormatter.FormatServerLog(message)
            // });
        }

    }

}