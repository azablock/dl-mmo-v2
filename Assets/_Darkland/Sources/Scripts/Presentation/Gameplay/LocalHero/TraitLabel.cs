using System.Linq;
using _Darkland.Sources.Models.Chat;
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
            var affectedStats = HeroStatsCalculator
                .statsFormulas[statId]
                .Keys
                .Aggregate(string.Empty, (res, it) => res + $"{it}\n");
            
            return new TooltipDescription {
                title = statId.ToString(),
                content = $"{generalDescription}\n\n" +
                          $"Affects stats:\n" +
                          $"{RichTextFormatter.Size(affectedStats, 12)}"
            };
        }

    }

}