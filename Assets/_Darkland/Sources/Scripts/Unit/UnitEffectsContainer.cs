using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.ScriptableObjects.Hero;
using _Darkland.Sources.ScriptableObjects.Unit;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitEffectsContainer : MonoBehaviour {

        public static UnitEffectsContainer _;
        
        private void Awake() {
            _ = this;
        }

        [SerializeField]
        private List<UnitEffect> unitEffects;

        public UnitEffect EffectByName(string effectName) {
            return unitEffects.First(it => it.EffectName.Equals(effectName));
        }
    }

}