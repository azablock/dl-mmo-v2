using System.Collections.Generic;
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
            for (var i = 0; i < backpackSlots.Count; i++) {
                var idx = i;
                backpackSlots[i].DropClick += () => OnDropClick(idx);
                backpackSlots[i].UseClick += () => OnUseClick(idx);
                backpackSlots[i].SellClick += () => OnSellClick(idx);
            }
        }

        private void OnDisable() {
            for (var i = 0; i < backpackSlots.Count; i++) {
                var idx = i;
                backpackSlots[i].DropClick -= () => OnDropClick(idx);
                backpackSlots[i].UseClick -= () => OnUseClick(idx);
                backpackSlots[i].SellClick -= () => OnSellClick(idx);
            }
        }

        [Client]
        public void ClientRefresh(List<string> itemNames) {
            Assert.IsTrue(itemNames.Count <= backpackSlots.Count);

            for (var i = 0; i < itemNames.Count; i++) {
                backpackSlots[i].ClientSet(itemNames[i]);
            }

            for (var i = itemNames.Count; i < backpackSlots.Count; i++) {
                backpackSlots[i].ClientClear();
            }
        }

        [Client]
        public void ClientUpdateGoldAmount(int goldAmount) => goldAmountText.text = $"{goldAmount}";

        [Client]
        private static void OnDropClick(int slotIdx) {
            NetworkClient.Send(new PlayerInputMessages.DropItemRequestMessage {backpackSlot = slotIdx});
        }

        [Client]
        private static void OnUseClick(int slotIdx) {
            NetworkClient.Send(new PlayerInputMessages.UseItemRequestMessage {backpackSlot = slotIdx});
        }

        [Client]
        private static void OnSellClick(int slotIdx) {
            NetworkClient.Send(new TradeMessages.SellItemRequestMessage() {backpackSlot = slotIdx});
        }

    }

}