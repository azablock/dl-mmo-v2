using _Darkland.Sources.Models;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Scripts.Bot {

    public class DarklandBot : NetworkBehaviour {

        private IAvailableMovesHolder _availableMovesHolder;

        private void Awake() {
            _availableMovesHolder = new SimpleAvailableMovesHolder();
        }

        public override void OnStartServer() {
            var isSingleName = Random.Range(0, 10) % 3 == 0;
            var heroName = isSingleName ? $"{CharacterNamesMock.RandomName()}" : $"{CharacterNamesMock.RandomName()} {CharacterNamesMock.RandomName()}";
            GetComponent<UnitNameBehaviour>().ServerSet(heroName);
        }

        public override void OnStartLocalPlayer() {
            var randomRepeatRate = Random.Range(1.5f, 3.0f);
            InvokeRepeating(nameof(ClientSendMoveRequest), 0.0f, randomRepeatRate);
        }

        [Client]
        private void ClientSendMoveRequest() {
            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {movementVector = _availableMovesHolder.NextMoveDelta()});
        }
    }

}