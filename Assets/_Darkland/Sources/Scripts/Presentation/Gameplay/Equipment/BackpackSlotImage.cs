using System;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using _Darkland.Sources.Scripts.Input;
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
        public event Action SellClick;
        public event Action UseClick;

        public void OnPointerClick(PointerEventData eventData) {
            if (_item == null) return;

            if (eventData.button == PointerEventData.InputButton.Left) {
                if (InputStateBehaviour._.tradeActive) {
                    SellClick?.Invoke();
                }
                else {
                    DropClick?.Invoke();
                }
            }

            if (eventData.button == PointerEventData.InputButton.Right) {
                //send message - equip weapon OR consume consumable
                UseClick?.Invoke();
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