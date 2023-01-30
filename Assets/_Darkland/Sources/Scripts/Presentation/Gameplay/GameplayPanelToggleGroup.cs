using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayPanelToggleGroup : MonoBehaviour {

        [SerializeField]
        private GameplayPanelToggle heroSheetToggle;
        [SerializeField]
        private GameplayPanelToggle equipmentToggle;

        [Client]
        public void ClientToggleHeroSheet() => heroSheetToggle.ClientToggle();

        [Client]
        public void ClientToggleEquipment() => equipmentToggle.ClientToggle();

    }

}