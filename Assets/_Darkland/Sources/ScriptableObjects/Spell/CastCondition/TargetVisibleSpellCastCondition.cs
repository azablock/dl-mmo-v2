using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.World;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(TargetVisibleSpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(TargetVisibleSpellCastCondition))]
    public class TargetVisibleSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster) {
            var currentPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = caster.GetComponent<ITargetNetIdHolder>().TargetPos();

            var bresenham3Dv2 = new Bresenham3Dv2(currentPos, targetPos);

            //NONE from given result is obstacle
            return !bresenham3Dv2
                .Cast<Vector3Int>()
                .Any(it => DarklandWorldBehaviour._.obstaclePositions.Contains(it));
        }

        public override string InvalidCastMessage() {
            return "Current target not in the light of sight";
        }

    }

}