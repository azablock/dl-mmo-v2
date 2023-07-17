using _Darkland.Sources.Scripts.Presentation.Gameplay;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class DarklandZoneTriggerBehaviour : MonoBehaviour {

        [Client]
        private void OnTriggerEnter(Collider other) {
            var darklandHero = other.GetComponent<DarklandHeroBehaviour>();

            if (darklandHero == null) return;
            if (!darklandHero.isLocalPlayer) return;

            GameplayRootPanel.LocalHeroZoneText.ClientShow("Tarmin Zone");
        }

    }

}