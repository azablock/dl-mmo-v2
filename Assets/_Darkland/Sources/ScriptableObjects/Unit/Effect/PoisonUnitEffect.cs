using System.Collections;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Unit.Effect {

    [CreateAssetMenu(fileName = nameof(PoisonUnitEffect),
                     menuName = "DL/"  + nameof(UnitEffect) + "/" + nameof(PoisonUnitEffect))]
    public class PoisonUnitEffect : UnitEffect {

        [SerializeField]
        private int poisonDamage;
        [SerializeField]
        private int poisonRepeats;
        [SerializeField]
        private int timeBetweenPoison;
        
        public override IEnumerator Process(GameObject effectHolder) {
            var repeats = poisonRepeats;

            while (repeats > 0) {
                effectHolder.GetComponent<IStatsHolder>().Subtract(StatId.Health, StatVal.OfBasic(poisonDamage));
                //todo server send poison vfx
                
                yield return new WaitForSeconds(timeBetweenPoison);
                repeats--;
            }
        }

        public override string Description(GameObject parent) {
            return $"Inflicts {poisonDamage} for {poisonRepeats} times.\n" +
                   $"Time between poison damage: {timeBetweenPoison}";
        }

        public override float Duration => poisonRepeats * timeBetweenPoison;

    }

}