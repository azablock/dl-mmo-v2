using System;
using System.Collections.Generic;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class BackpackPanel : MonoBehaviour {

        [SerializeField]
        private List<BackpackSlotImage> backpackSlots;

        private void OnEnable() {
            for (var i = 0; i < backpackSlots.Count; i++) {
                var idx = i;
                backpackSlots[i].DropClick += () => OnDropClick(idx);
            }
        }

        private void OnDisable() {
            for (var i = 0; i < backpackSlots.Count; i++) {
                var idx = i;
                backpackSlots[i].DropClick -= () => OnDropClick(idx);
            }
        }

        public void ClientRefresh(List<string> itemNames) {
            Assert.IsTrue(itemNames.Count <= backpackSlots.Count);

            for (var i = 0; i < itemNames.Count; i++) {
                backpackSlots[i].ClientSet(itemNames[i]);
            }

            for (var i = itemNames.Count; i < backpackSlots.Count; i++) {
                backpackSlots[i].ClientClear();
            }
        }

        private static void OnDropClick(int slotIdx) {
            NetworkClient.Send(new PlayerInputMessages.DropItemRequestMessage {backpackSlot = slotIdx});
        }

    }

}