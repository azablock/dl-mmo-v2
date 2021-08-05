using _Darkland.Sources.NetworkMessages;
using Mirror;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Scripts {

    public class DarklandBot : NetworkBehaviour {

        public override void OnStartLocalPlayer() {
            var randomRepeatRate = Random.Range(1.5f, 3.0f);
            InvokeRepeating(nameof(ClientSendActionRequest), 0.0f, randomRepeatRate);
        }

        [Client]
        private void ClientSendActionRequest() {
            NetworkClient.Send(new DarklandPlayerMessages.ActionRequestMessage {darklandPlayerNetId = netId});
        }
    }
}