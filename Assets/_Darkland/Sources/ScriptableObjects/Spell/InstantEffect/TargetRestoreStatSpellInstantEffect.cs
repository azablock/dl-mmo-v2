using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(TargetRestoreStatSpellInstantEffect),
                     menuName = "DL/" + nameof(SpellInstantEffect) + "/" + nameof(TargetRestoreStatSpellInstantEffect))]
    public class TargetRestoreStatSpellInstantEffect : SpellInstantEffect {

        [SerializeField]
        private StatId restoreStatId;
        [SerializeField]
        private int restoreAmount;

        public override void Process(GameObject caster) {
            var actionPower = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current;
            var target = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var resultRestoreAmount = restoreAmount + actionPower;
            target.GetComponent<IStatsHolder>().Add(restoreStatId, StatVal.OfBasic(resultRestoreAmount));

            //todo decouple
            switch (restoreStatId) {
                case StatId.Mana:
                    NetworkServer.SendToReady(new SpellMessages.TransferManaSpellVfxResponseMessage {
                        castPos = caster.GetComponent<IDiscretePosition>().Pos,
                        targetPos = target.GetComponent<IDiscretePosition>().Pos
                    });
                    break;
                case StatId.Health:
                    NetworkServer.SendToReady(new SpellMessages.HealSpellVfxResponseMessage {
                        targetPos = target.GetComponent<IDiscretePosition>().Pos
                    });
                    break;
            }

        }

        public override string Description(GameObject caster) {
            var desc = "";
            var actionPower = caster.GetComponent<IStatsHolder>().ValueOf(StatId.ActionPower).Current;

            if (restoreStatId == StatId.Mana)
                desc += $"Transfer {restoreAmount + actionPower} mana to target.";
            else if (restoreStatId == StatId.Health) desc += $"Restore {restoreAmount + actionPower} health to target.";

            return desc;
        }

    }

}