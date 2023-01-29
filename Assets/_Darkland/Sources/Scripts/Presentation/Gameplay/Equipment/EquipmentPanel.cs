using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Equipment {

    public class EquipmentPanel : MonoBehaviour {

        [SerializeField]
        private BackpackPanel backpackPanel;

        private void OnEnable() {
            DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>().ClientBackpackChanged += ClientOnBackpackChanged;
            DarklandHeroMessagesProxy.ClientGetEq += ClientOnGetEq;
            
            NetworkClient.Send(new DarklandHeroMessages.GetEqRequestMessage());
        }

        private void OnDisable() {
            DarklandHeroBehaviour.localHero.GetComponent<IEqChangeServerListener>().ClientBackpackChanged -= ClientOnBackpackChanged;
            DarklandHeroMessagesProxy.ClientGetEq -= ClientOnGetEq;
        }

        [Client]
        private void ClientOnBackpackChanged(List<string> itemNames) => backpackPanel.ClientRefresh(itemNames);

        [Client]
        private void ClientOnGetEq(DarklandHeroMessages.GetEqResponseMessage message) =>
            backpackPanel.ClientRefresh(message.itemNames);

    }

}