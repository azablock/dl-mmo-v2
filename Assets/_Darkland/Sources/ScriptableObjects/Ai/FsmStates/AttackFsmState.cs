using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Presentation.Unit;
using _Darkland.Sources.Scripts.Unit.Combat;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(AttackFsmState), menuName = "DL/Ai/" + nameof(AttackFsmState))]
    public class AttackFsmState : FsmState {

        public override void UpdateSelf(GameObject parent) {
            var aiDarklandUnit = parent.GetComponent<DarklandUnit>();
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var damageDealer = parent.GetComponent<IDamageDealer>();
            var playerNetIdentity = targetNetIdHolder.TargetNetIdentity;

            if (!playerNetIdentity) return;
            
            //todo tutaj dodatkowy check bo byl bug ze szur atakowal gracza stojacego pietro wyzej
            var targetPlayerPos = playerNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var parentPos = aiDarklandUnit.GetComponent<IDiscretePosition>().Pos;
            Assert.IsTrue(targetPlayerPos.z == parentPos.z);

            var mobDef = parent.GetComponent<IMobDefHolder>().MobDef;
            var damage = Random.Range(mobDef.MinDamage, mobDef.MaxDamage);
            
            damageDealer.DealDamage(new UnitAttackEvent {
                damage = damage,
                target = playerNetIdentity,
                damageType = mobDef.DamageType
            });
            
            parent.GetComponent<AiCombatMemory>().Add(playerNetIdentity);
            
            // //todo gdzies indziej to wyniesc - dalsza czesc combatu...
            // //todo to jest funkcjonalnosc ze jak gracz zostaje zaatakowany i nie ma celu to cel sie auto wybiera na przeciwnika ktory zaatakowal
            var playerTargetNetIdeHolder = playerNetIdentity.GetComponent<ITargetNetIdHolder>();
            
            if (playerTargetNetIdeHolder.TargetNetIdentity == null) {
                playerTargetNetIdeHolder.Set(aiDarklandUnit.netId);
            }
        }

    }

}