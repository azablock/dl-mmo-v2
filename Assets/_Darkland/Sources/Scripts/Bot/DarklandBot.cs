using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Ai;
using Mirror;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Scripts.Bot {

    public class DarklandBot : NetworkBehaviour {

        private AvailableMovesDummyHandler _availableMovesDummyHandler;

        private void Awake() {
            _availableMovesDummyHandler = GetComponent<AvailableMovesDummyHandler>();
        }

        public override void OnStartLocalPlayer() {
            var randomRepeatRate = Random.Range(1.5f, 3.0f);
            InvokeRepeating(nameof(ClientSendMoveRequest), 0.0f, randomRepeatRate);
        }

        [Client]
        private void ClientSendMoveRequest() {
            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {movementVector = _availableMovesDummyHandler.ClientNextMoveDelta()});
        }
    }

}