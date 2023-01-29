using System;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class BackpackSlotImage : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private Sprite emptySlotSprite;
        [SerializeField]
        private Color emptySlotBackgroundColor;
        [SerializeField]
        private Image image;
        private IEqItemDef _item;

        public event Action DropClick;

        public void OnPointerClick(PointerEventData eventData) {
            if (_item == null) return;

            if (eventData.button == PointerEventData.InputButton.Left) {
                DropClick?.Invoke();
                //send message - drop on ground
            }

            if (eventData.button == PointerEventData.InputButton.Right) {
                //send message - equip weapon OR consume consumable
            }
        }

        [Client]
        public void ClientSet(string itemName) {
            _item = EqItemsContainer.ItemDef2(itemName);
            image.sprite = _item.Sprite;
            image.color = Color.white;
        }

        [Client]
        public void ClientClear() {
            _item = null;
            image.sprite = emptySlotSprite;
            image.color = emptySlotBackgroundColor;
        }

    }

}