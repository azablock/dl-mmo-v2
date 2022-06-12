using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public class DarklandStatsEffectHandler : NetworkBehaviour {
        private PersistentDarklandStatsEffect _effect;

        private IDarklandStatsHolder _darklandStatsHolder;
        
        public override void OnStartServer() {
            if (_effect.CanBeApplied(_darklandStatsHolder)) {
                StartCoroutine(_effect.Apply(_darklandStatsHolder));
            }
        }

        public override void OnStopServer() {
            StopAllCoroutines();
        }

        public void ApplyDirectEffect(DirectDarklandStatsEffect effect) {
            if (effect.CanBeApplied(_darklandStatsHolder)) {
                effect.Apply(_darklandStatsHolder);
            }
        }
    }

}