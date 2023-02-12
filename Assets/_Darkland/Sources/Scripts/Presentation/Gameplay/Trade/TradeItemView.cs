using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeItemView : MonoBehaviour, IPointerClickHandler, IDescriptionProvider {

        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text itemNameText;
        [SerializeField]
        private TMP_Text itemPriceText;

        private IEqItemDef _item;

        public void OnPointerClick(PointerEventData _) {
            if (_item == null) return;
            
            NetworkClient.Send(new TradeMessages.BuyItemRequestMessage {itemName = _item.ItemName});
        }

        [Client]
        public void ClientSet(IEqItemDef item) {
            Assert.IsNotNull(item);
            _item = item;

            itemImage.sprite = item.Sprite;
            itemNameText.text = item.ItemName;
            itemPriceText.text = $"{item.ItemPrice}";
        }

        [Client]
        public void ClientRefreshPricePoint(in int goldAmount) {
            if (_item == null) return;

            var color = _item.ItemPrice > goldAmount ? DarklandColorSet._.danger : Color.white;
            itemPriceText.color = color;
        }

        public TooltipDescription Get() {
            return new TooltipDescription {
                title = _item?.ItemName,
                content = _item?.Description(DarklandHeroBehaviour.localHero.gameObject)
            };
        }

    }

}