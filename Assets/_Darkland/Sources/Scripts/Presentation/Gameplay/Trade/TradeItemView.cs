using _Darkland.Sources.Models.Equipment;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeItemView : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private Image itemImage;
        [SerializeField]
        private TMP_Text priceText;

        private IEqItemDef _item;

        [Client]
        public void ClientSet(IEqItemDef item) {
            Assert.IsNotNull(item);
            _item = item;

            itemImage.sprite = item.Sprite;
            priceText.text = $"{item.ItemPrice}";
        }

        public void OnPointerClick(PointerEventData _) {
            
        }

    }

}