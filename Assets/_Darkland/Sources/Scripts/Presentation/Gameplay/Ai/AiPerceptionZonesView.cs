using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Ai {

    public class AiPerceptionZonesView : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer attackPerceptionSpriteRenderer;
        [SerializeField]
        private SpriteRenderer passivePerceptionSpriteRenderer;
        [SerializeField]
        private AiNetworkPerceptionBehaviour2 aiNetworkPerception;

        private void Start() {
            var passiveRange = aiNetworkPerception.PerceptionZoneRange(AiPerceptionZoneType.Passive);
            var passiveViewScale = new Vector3(passiveRange, passiveRange, 1.0f);
            passivePerceptionSpriteRenderer.transform.localScale = passiveViewScale;
            
            var attackRange = aiNetworkPerception.PerceptionZoneRange(AiPerceptionZoneType.Attack);
            var attackViewScale = new Vector3(attackRange, attackRange, 1.0f);
            attackPerceptionSpriteRenderer.transform.localScale = attackViewScale;
        }

    }

}