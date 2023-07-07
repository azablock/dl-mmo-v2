using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.LocalHero {

    public class TraitDistributionImage : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private StatId traitStatId;

        public void OnPointerClick(PointerEventData _) {
            NetworkClient.Send(new DarklandHeroMessages.DistributeTraitRequestMessage { traitStatId = traitStatId });
        }

    }

}