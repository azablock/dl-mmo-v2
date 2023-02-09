using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
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
            var target = caster.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            target.GetComponent<IStatsHolder>().Add(restoreStatId, StatVal.OfBasic(restoreAmount));

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

    }

}