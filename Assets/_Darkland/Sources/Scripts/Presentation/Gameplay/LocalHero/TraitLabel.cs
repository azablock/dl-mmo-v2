using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class TraitLabel : MonoBehaviour, IDescriptionProvider {

        [SerializeField]
        private StatId statId;

        [SerializeField]
        [TextArea]
        private string generalDescription;

        public TooltipDescription Get() {
            var str = "";

            foreach (var (affectedStatId, fn) in HeroStatsCalculator.statsFormulas[statId]) {
                var castsToIntStr = HeroStatsCalculator.statIdsFlooredToInt.Contains(affectedStatId)
                    ? "(casts to floor int)"
                    : "";
                var fnFormula = fn(1.0f);
                var fnFormulaFormatted = RichTextFormatter.Bold($"{fnFormula:0.00}");

                str += $"adds {fnFormulaFormatted} points to {affectedStatId.ToString()} {castsToIntStr}\n";
            }

            // var affectedStats = HeroStatsCalculator
            //     .statsFormulas[statId]
            //     .Keys
            //     .Aggregate(string.Empty, (res, it) => res + $"{it}\n");

            return new TooltipDescription {
                title = statId.ToString(),
                content = $"{generalDescription}\n\n" +
                          "Affects stats:\n" +
                          $"{RichTextFormatter.Size(str, 14)}"
            };
        }

    }

}