using System.Linq;
using _Darkland.Sources.NetworkMessages;
using Mirror;

namespace _Darkland.Sources.Scripts {

    public class SentActionRequestMessagesCountHolder : NetworkBehaviour {

        public int count { private set; get; }

        public override void OnStartLocalPlayer() {
            CmdGetHolderCounts();
        }

        [Server]
        public void ServerIncrement() {
            count++;
        }

        [Command]
        private void CmdGetHolderCounts() {
            var holders = FindObjectsOfType<SentActionRequestMessagesCountHolder>().Where(holder => holder.GetComponent<SentActionRequestMessagesCountHolder>());
            
            foreach (var it in holders) {
                connectionToClient.Send(new DarklandPlayerMessages.ActionResponseMessage {
                    darklandPlayerNetId = it.netId,
                    sentActionRequestMessagesCount = it.count
                });
            }
        }
    }

}