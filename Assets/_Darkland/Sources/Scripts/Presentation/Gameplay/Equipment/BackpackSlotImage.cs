using System;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using _Darkland.Sources.Scripts.Input;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class BackpackSlotImage : MonoBehaviour, IPointerClickHandler, IDescriptionProvider {

        [SerializeField]
        private Sprite emptySlotSprite;
        [SerializeField]
        private Color emptySlotBackgroundColor;
        [SerializeField]
        private Image image;

        private IEqItemDef _item;

        public TooltipDescription Get() {
            return new() {
                title = _item?.ItemName,
                content = _item?.Description(DarklandHeroBehaviour.localHero.gameObject)
            };
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (_item == null) return;

            switch (eventData.button) {
                case PointerEventData.InputButton.Left when InputStateBehaviour._.tradeActive:
                    SellClick?.Invoke(this);
                    break;
                case PointerEventData.InputButton.Left:
                    DropClick?.Invoke(this);
                    break;
                case PointerEventData.InputButton.Right:
                    //send message - equip weapon OR consume consumable
                    UseClick?.Invoke(this);
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public event Action<BackpackSlotImage> DropClick;
        public event Action<BackpackSlotImage> SellClick;
        public event Action<BackpackSlotImage> UseClick;

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