using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class EquipmentPanel : MonoBehaviour {

        [SerializeField]
        private BackpackPanel backpackPanel;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped += DarklandHeroOnLocalHeroStopped;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped -= DarklandHeroOnLocalHeroStopped;
        }

        private void DarklandHeroOnLocalHeroStarted() {
            DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>().ClientBackpackChanged += ClientOnBackpackChanged;
        }

        private void DarklandHeroOnLocalHeroStopped() {
            DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>().ClientBackpackChanged -= ClientOnBackpackChanged;
        }

        [Client]
        private void ClientOnBackpackChanged(List<string> itemNames) => backpackPanel.ClientRefresh(itemNames);

    }

}