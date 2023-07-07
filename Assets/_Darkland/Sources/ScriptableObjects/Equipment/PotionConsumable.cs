using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(Consumable),
                     menuName = "DL/Eq/" + nameof(Consumable) + "/" + nameof(PotionConsumable))]
    public class PotionConsumable : Consumable {

        [Header("Consumable Specific")]
        public int regenValue;
        public StatId statToRegen;

        public override void Consume(GameObject eqHolder) {
            eqHolder
                .GetComponent<IStatsHolder>()
                .Add(statToRegen, StatVal.OfBasic(regenValue));
        }

        public override string Description(GameObject parent) {
            return $"Restores {RichTextFormatter.Bold(regenValue.ToString())} points of {statToRegen.ToString()}.";
        }

    }

}