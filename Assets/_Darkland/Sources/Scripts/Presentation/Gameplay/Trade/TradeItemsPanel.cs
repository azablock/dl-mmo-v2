using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Trade {

    public class TradeItemsPanel : MonoBehaviour {

        [SerializeField]
        private GameObject tradeItemViewPrefab;
        
        public static event Action Toggled;

        [Client]
        public void ClientSet(List<IEqItemDef> items) {
            items.ForEach(it => 
                              Instantiate(tradeItemViewPrefab, transform)
                              .GetComponent<TradeItemView>()
                              .ClientSet(it));
            
            Toggled?.Invoke();
        }

        [Client]
        public void ClientClear() {
            foreach (Transform child in transform) Destroy(child.gameObject);
            Toggled?.Invoke();
        }

    }

}