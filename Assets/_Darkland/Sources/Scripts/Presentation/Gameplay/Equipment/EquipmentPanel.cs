using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class EquipmentPanel : MonoBehaviour {

        [SerializeField]
        private BackpackPanel backpackPanel;
        [SerializeField]
        private EquippedWearablesPanel equippedWearablesPanel;

        private void OnEnable() {
            var eqChangeServerListener = DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>();
            eqChangeServerListener.ClientBackpackChanged += ClientOnBackpackChanged;
            eqChangeServerListener.ClientWearableEquipped += ClientOnWearableEquipped;
            eqChangeServerListener.ClientWearableCleared += ClientOnWearableCleared;

            var goldHolder = DarklandHeroBehaviour.localHero.GetComponent<IGoldHolder>();
            goldHolder.ClientGoldAmountChanged += ClientOnGoldAmountChanged;
                
            DarklandHeroMessagesProxy.ClientGetEq += ClientOnGetEq;
            
            NetworkClient.Send(new DarklandHeroMessages.GetEqRequestMessage());
        }

        private void OnDisable() {
            var eqChangeServerListener = DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>();
            eqChangeServerListener.ClientBackpackChanged -= ClientOnBackpackChanged;
            eqChangeServerListener.ClientWearableEquipped -= ClientOnWearableEquipped;
            eqChangeServerListener.ClientWearableCleared -= ClientOnWearableCleared;

            var goldHolder = DarklandHeroBehaviour.localHero.GetComponent<IGoldHolder>();
            goldHolder.ClientGoldAmountChanged -= ClientOnGoldAmountChanged;

            DarklandHeroMessagesProxy.ClientGetEq -= ClientOnGetEq;
        }

        [Client]
        private void ClientOnBackpackChanged(List<string> itemNames) => backpackPanel.ClientRefresh(itemNames);

        [Client]
        private void ClientOnGetEq(DarklandHeroMessages.GetEqResponseMessage message) {
            backpackPanel.ClientRefresh(message.itemNames);
            backpackPanel.ClientUpdateGoldAmount(message.goldAmount);
            message.equippedWearables.ForEach(it => ClientOnWearableEquipped(it.wearableSlot, it.itemName));
        }

        [Client]
        private void ClientOnWearableEquipped(WearableSlot wearableSlot, string itemName) {
            equippedWearablesPanel.ClientSet(wearableSlot, itemName);
        }

        [Client]
        private void ClientOnWearableCleared(WearableSlot wearableSlot) {
            equippedWearablesPanel.ClientClear(wearableSlot);
        }

        [Client]
        private void ClientOnGoldAmountChanged(int goldAmount) => backpackPanel.ClientUpdateGoldAmount(goldAmount);

    }

}