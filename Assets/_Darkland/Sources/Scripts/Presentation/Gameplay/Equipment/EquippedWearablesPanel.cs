using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class EquippedWearablesPanel : MonoBehaviour {

        [SerializeField]
        private List<WearableSlotImage> wearableSlotImages;

        [Client]
        public void ClientSet(WearableSlot wearableSlot, string itemName) {
            var wearableSlotImage = wearableSlotImages.FirstOrDefault(it => it.Slot == wearableSlot);
            Assert.IsNotNull(wearableSlotImage);

            wearableSlotImage.ClientSet(itemName);
        }

        [Client]
        public void ClientClear(WearableSlot wearableSlot) {
            var wearableSlotImage = wearableSlotImages.FirstOrDefault(it => it.Slot == wearableSlot);
            Assert.IsNotNull(wearableSlotImage);

            wearableSlotImage.ClientClear();
        }

    }

}