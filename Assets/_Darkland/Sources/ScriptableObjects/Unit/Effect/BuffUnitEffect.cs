using System.Collections;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Unit.Effect {

    [CreateAssetMenu(fileName = nameof(BuffUnitEffect),
                     menuName = "DL/"  + nameof(UnitEffect) + "/" + nameof(BuffUnitEffect))]
    public class BuffUnitEffect : UnitEffect {

        public StatId statId;
        public float buffValue;

        public override void PreProcess(GameObject effectHolder) {
            effectHolder.GetComponent<IStatsHolder>().Add(statId, StatVal.OfBonus(buffValue));
        }

        public override IEnumerator Process(GameObject effectHolder) {
            yield return new WaitForSeconds(duration);
        }

        public override void PostProcess(GameObject effectHolder) {
            effectHolder.GetComponent<IStatsHolder>().Subtract(statId, StatVal.OfBonus(buffValue));
        }

        public override string Description(GameObject parent) {
            return $"Increases {statId.ToString()} by {buffValue} points.\n" +
                   $"Duration:\t{Duration} seconds (base duration)";
        }

    }

}