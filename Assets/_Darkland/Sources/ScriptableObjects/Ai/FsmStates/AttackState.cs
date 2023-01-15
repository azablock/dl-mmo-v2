using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Presentation.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(AttackState), menuName = "DL/Ai/" + nameof(AttackState))]
    public class AttackState : FsmState {

        public override void UpdateSelf(GameObject parent) {
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var playerNetIdentity = targetNetIdHolder.TargetNetIdentity;
            var healthStat = playerNetIdentity.GetComponent<IStatsHolder>().Stat(StatId.Health);

            healthStat.Set(healthStat.Get() - 3);

            // var message = ChatMessagesFormatter.FormatServerLog($"AttackState (dmg 2)");
            // NetworkServer.SendToReady(new ChatMessages.ServerLogResponseMessage() {message = message});

            
            
            
            //todo gdzies indziej to wyniesc - dalsza czesc combatu...
            var playerTargetNetIdeHolder = playerNetIdentity.GetComponent<ITargetNetIdHolder>();
            var aiDarklandUnit = parent.GetComponent<DarklandUnit>();

            if (playerTargetNetIdeHolder.TargetNetIdentity == null) {
                playerTargetNetIdeHolder.Set(aiDarklandUnit.netId);
            }
        }

    }

}