using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayPanelToggleGroup : MonoBehaviour {

        [SerializeField]
        private GameplayPanelToggle heroSheetToggle;

        [Client]
        public void ClientToggleHeroSheet() => heroSheetToggle.ClientToggle();

    }

}