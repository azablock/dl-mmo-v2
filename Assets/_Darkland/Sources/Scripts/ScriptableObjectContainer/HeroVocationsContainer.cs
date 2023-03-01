using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.ScriptableObjects.Hero;
using UnityEngine;

namespace _Darkland.Sources.Scripts.ScriptableObjectContainer {

    public class HeroVocationsContainer : MonoBehaviour {

        public static HeroVocationsContainer _;
        
        private void Awake() {
            _ = this;
        }

        [SerializeField]
        private List<HeroVocation> vocations;

        public HeroVocation Vocation(HeroVocationType type) {
            return vocations.First(it => it.VocationType == type);
        }
    }

}