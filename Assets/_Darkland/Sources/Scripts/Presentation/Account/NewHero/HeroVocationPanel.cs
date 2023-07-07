using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
using Castle.Core.Internal;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Account.NewHero {

    public class HeroVocationPanel : MonoBehaviour {

        [SerializeField]
        private TMP_Text vocationText;

        [SerializeField]
        private List<HeroVocationSelectable> vocationSelectables;

        private void OnEnable() {
            Assert.IsFalse(vocationSelectables.IsNullOrEmpty());
            vocationSelectables.ForEach(it => it.Clicked += OnVocationClicked);

            OnVocationClicked(vocationSelectables.First().VocationType);
        }

        private void OnDisable() {
            Assert.IsFalse(vocationSelectables.IsNullOrEmpty());
            vocationSelectables.ForEach(it => it.Clicked -= OnVocationClicked);
        }

        public event Action<HeroVocationType> VocationSelected;

        private void OnVocationClicked(HeroVocationType vocationType) {
            vocationText.text = vocationType.ToString();
            VocationSelected?.Invoke(vocationType);
        }

    }

}