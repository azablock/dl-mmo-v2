using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Equipment {

    [CreateAssetMenu(fileName = nameof(Consumable),
                     menuName = "DL/Eq/"  + nameof(Consumable) + "/" + nameof(HealthPotionConsumable))]
    public class HealthPotionConsumable : Consumable {

        [Header("Consumable Specific")]
        public int healthToRegen;
        
        public override void Consume(GameObject eqHolder) {
            eqHolder
                .GetComponent<IStatsHolder>()
                .Add(StatId.Health, StatVal.OfBasic(healthToRegen));
        }

        public override string Description(GameObject parent) {
            return $"Restores {RichTextFormatter.Bold(healthToRegen.ToString())} points of health.";
        }

    }

}