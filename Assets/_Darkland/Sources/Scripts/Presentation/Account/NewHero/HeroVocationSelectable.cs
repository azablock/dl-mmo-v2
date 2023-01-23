using System;
using _Darkland.Sources.Models.Hero;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation.Account.NewHero {

    public class HeroVocationSelectable : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private HeroVocation vocation;

        public event Action<HeroVocation> Clicked;

        public void OnPointerClick(PointerEventData _) => Clicked?.Invoke(vocation);
    }

}