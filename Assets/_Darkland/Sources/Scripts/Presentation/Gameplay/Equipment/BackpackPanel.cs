using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class BackpackPanel : MonoBehaviour {

        [SerializeField]
        private List<BackpackSlotImage> backpackSlots;

        public void ClientRefresh(List<string> itemNames) {
            Assert.IsTrue(itemNames.Count <= backpackSlots.Count);

            for (var i = 0; i < itemNames.Count; i++) {
                backpackSlots[i].ClientSet(itemNames[i]);
            }

            for (var i = itemNames.Count; i < backpackSlots.Count; i++) {
                backpackSlots[i].ClientClear();
            }
        }
    }

}