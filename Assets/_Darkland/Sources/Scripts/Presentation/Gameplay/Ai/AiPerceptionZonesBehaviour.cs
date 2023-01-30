using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Ai {

    public class AiPerceptionZonesBehaviour : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer attackPerceptionSpriteRenderer;
        [SerializeField]
        private AiNetworkPerceptionBehaviour aiNetworkPerception;

        private void Awake() {
            var scale = new Vector3(aiNetworkPerception.AttackPerceptionRange,
                                    aiNetworkPerception.AttackPerceptionRange,
                                    1.0f);
            
            attackPerceptionSpriteRenderer.transform.localScale = scale;
        }

    }

}