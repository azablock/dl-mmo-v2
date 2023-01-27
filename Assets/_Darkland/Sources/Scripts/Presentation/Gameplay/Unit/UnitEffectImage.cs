using _Darkland.Sources.ScriptableObjects.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class UnitEffectImage : MonoBehaviour {

        [SerializeField]
        private Image image;
        [SerializeField]
        private UnitEffect unitEffect;

        public string unitEffectName => unitEffect.EffectName;

        [Client]
        public void ClientInit(Sprite sprite) => image.sprite = sprite;

    }

}