using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class BackpackPanel : MonoBehaviour {

        [SerializeField]
        private List<BackpackSlotImage> backpackSlots;
        [SerializeField]
        private TMP_Text goldAmountText;

        private void OnEnable() {
            foreach (var it in backpackSlots) {
                it.DropClick += OnDropClick;
                it.UseClick += OnUseClick;
                it.SellClick += OnSellClick;
            }
        }

        private void OnDisable() {
            foreach (var it in backpackSlots) {
                it.DropClick -= OnDropClick;
                it.UseClick -= OnUseClick;
                it.SellClick -= OnSellClick;
            }
        }

        [Client]
        public void ClientRefresh(List<string> itemNames) {
            Assert.IsTrue(itemNames.Count <= backpackSlots.Count);

            for (var i = 0; i < itemNames.Count; i++) backpackSlots[i].ClientSet(itemNames[i]);

            for (var i = itemNames.Count; i < backpackSlots.Count; i++) backpackSlots[i].ClientClear();
        }

        [Client]
        public void ClientUpdateGoldAmount(int goldAmount) {
            goldAmountText.text = $"{goldAmount}";
        }

        [Client]
        private void OnDropClick(BackpackSlotImage image) {
            var slotIdx = IndexOfBackpackSlotImage(image);
            NetworkClient.Send(new PlayerInputMessages.DropItemRequestMessage { backpackSlot = slotIdx });
        }

        [Client]
        private void OnUseClick(BackpackSlotImage image) {
            var slotIdx = IndexOfBackpackSlotImage(image);
            NetworkClient.Send(new PlayerInputMessages.UseItemRequestMessage { backpackSlot = slotIdx });
        }

        [Client]
        private void OnSellClick(BackpackSlotImage image) {
            var slotIdx = IndexOfBackpackSlotImage(image);
            NetworkClient.Send(new TradeMessages.SellItemRequestMessage { backpackSlot = slotIdx });
        }

        private int IndexOfBackpackSlotImage(BackpackSlotImage backpackSlotImage) {
            return GetComponentsInChildren<BackpackSlotImage>().ToList().IndexOf(backpackSlotImage);
        }

    }

}