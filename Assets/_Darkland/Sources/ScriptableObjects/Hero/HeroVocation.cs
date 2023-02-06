using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.ScriptableObjects.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Hero {

    [CreateAssetMenu(fileName = nameof(HeroVocation), menuName = "DL/"  + nameof(HeroVocation))]
    public class HeroVocation : ScriptableObject, IHeroVocation {

        [SerializeField]
        private HeroVocationType vocationType;
        [SerializeField]
        private List<SpellDef> availableSpells;
        [SerializeField]
        [Obsolete]
        [Tooltip("currently not used")]
        private HeroVocationStartingStats startingStats;

        public HeroVocationType VocationType => vocationType;
        public List<ISpell> AvailableSpells => new(availableSpells);
        public IHeroVocationStartingStats StartingStats => startingStats;

    }

}