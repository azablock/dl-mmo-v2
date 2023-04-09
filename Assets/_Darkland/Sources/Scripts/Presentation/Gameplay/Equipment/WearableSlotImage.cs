using System;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Equipment;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class WearableSlotImage : MonoBehaviour, IPointerClickHandler, IDescriptionProvider {

        [SerializeField]
        private Image image;
        [SerializeField]
        private WearableSlot wearableSlot;
        [SerializeField]
        private WearableType wearableType;
        [SerializeField]
        private Sprite emptySlotSprite;
        [SerializeField]
        private Color emptySlotBackgroundColor;

        private IEqItemDef _item;

        public void OnPointerClick(PointerEventData eventData) {
            if (_item == null) return;
            if (eventData.button != PointerEventData.InputButton.Right) return;
            
            NetworkClient.Send(new PlayerInputMessages.UnequipWearableRequestMessage{wearableSlot = wearableSlot});
        }

        [Client]
        public void ClientSet(string itemName) {
            _item = EqItemsContainer.ItemDef2(itemName);
            Assert.IsTrue(_item.ItemType == EqItemType.Wearable);

            var wearable = (IWearable)_item;
            Assert.IsTrue(wearable.WearableItemType == wearableType);
            Assert.IsTrue(wearable.WearableItemSlot == wearableSlot);
            
            image.sprite = _item.Sprite;
            image.color = Color.white;
        }

        [Client]
        public void ClientClear() {
            _item = null;
            image.sprite = emptySlotSprite;
            image.color = emptySlotBackgroundColor;
        }

        public WearableSlot Slot => wearableSlot;

        public TooltipDescription Get() {
            return new TooltipDescription() {
                title = _item?.ItemName,
                content = _item?.Description(DarklandHeroBehaviour.localHero.gameObject)
            };
        }

    }

}