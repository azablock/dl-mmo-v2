using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeRootPanel : MonoBehaviour {

        [SerializeField]
        private TradeItemsPanel tradeItemsPanel;

        public TradeItemsPanel TradeItemsPanel => tradeItemsPanel;

    }

}