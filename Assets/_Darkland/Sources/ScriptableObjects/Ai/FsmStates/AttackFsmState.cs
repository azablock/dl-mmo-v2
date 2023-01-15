using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Presentation.Unit;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(AttackFsmState), menuName = "DL/Ai/" + nameof(AttackFsmState))]
    public class AttackFsmState : FsmState {

        public override void UpdateSelf(GameObject parent) {
            var aiDarklandUnit = parent.GetComponent<DarklandUnit>();
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var playerNetIdentity = targetNetIdHolder.TargetNetIdentity;
            var healthStat = playerNetIdentity.GetComponent<IStatsHolder>().Stat(StatId.Health);

            //todo tutaj dodatkowy check bo byl bug ze szur atakowal gracza stojacego pietro wyzej
            var targetPlayerPos = playerNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var parentPos = aiDarklandUnit.GetComponent<IDiscretePosition>().Pos;
            Assert.IsTrue(targetPlayerPos.z != parentPos.z);

            healthStat.Set(healthStat.Get() - 3);

            //todo gdzies indziej to wyniesc - dalsza czesc combatu...
            //todo to jest funkcjonalnosc ze jak gracz zostaje zaatakowany i nie ma celu to cel sie auto wybiera na przeciwnika ktory zaatakowal
            var playerTargetNetIdeHolder = playerNetIdentity.GetComponent<ITargetNetIdHolder>();

            if (playerTargetNetIdeHolder.TargetNetIdentity == null) {
                playerTargetNetIdeHolder.Set(aiDarklandUnit.netId);
            }
        }

    }

}