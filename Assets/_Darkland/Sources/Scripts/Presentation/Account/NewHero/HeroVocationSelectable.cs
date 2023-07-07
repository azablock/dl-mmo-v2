using System;
using _Darkland.Sources.Models.Hero;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation.Account.NewHero {

    public class HeroVocationSelectable : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private HeroVocationType vocationType;

        public HeroVocationType VocationType => vocationType;

        public void OnPointerClick(PointerEventData _) {
            Clicked?.Invoke(vocationType);
        }

        public event Action<HeroVocationType> Clicked;

    }

}