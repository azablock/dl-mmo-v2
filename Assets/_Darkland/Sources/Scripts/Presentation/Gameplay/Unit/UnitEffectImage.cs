using _Darkland.Sources.ScriptableObjects.Unit;
using _Darkland.Sources.Scripts.ScriptableObjectContainer;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class UnitEffectImage : MonoBehaviour, IDescriptionProvider {

        [SerializeField]
        private Image image;

        private UnitEffect _unitEffect;

        public TooltipDescription Get() {
            return new TooltipDescription {
                title = _unitEffect.EffectName,
                content = _unitEffect.Description(DarklandHeroBehaviour.localHero.gameObject)
            };
        }

        [Client]
        public void ClientInit(string effectName) {
            _unitEffect = UnitEffectsContainer._.EffectByName(effectName);
            image.sprite = _unitEffect.EffectSprite;
        }

    }

}