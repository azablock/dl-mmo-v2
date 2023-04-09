using _Darkland.Sources.Scripts.Unit;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeRootPanel : MonoBehaviour {

        [SerializeField]
        private TradeItemsPanel tradeItemsPanel;

        public TradeItemsPanel TradeItemsPanel => tradeItemsPanel;

        private void OnEnable() {
            LocalHeroGoldHolder.ClientGoldAmountChanged += ClientOnGoldAmountChanged;
            //todo LocalHeroGoldHolder.GoldAmount <- this is [SyncVar], rename it to indicate that?
        }

        private void OnDisable() {
            LocalHeroGoldHolder.ClientGoldAmountChanged -= ClientOnGoldAmountChanged;
        }

        private void ClientOnGoldAmountChanged(int goldAmount) => tradeItemsPanel.ClientRefreshPricePoints(goldAmount);

        private static IGoldHolder LocalHeroGoldHolder => DarklandHeroBehaviour.localHero.GetComponent<IGoldHolder>();

    }

}