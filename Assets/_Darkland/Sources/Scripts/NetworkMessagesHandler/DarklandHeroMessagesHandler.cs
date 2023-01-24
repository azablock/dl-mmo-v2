using System;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class DarklandHeroMessagesHandler : MonoBehaviour {

        private void Awake() {
            DarklandHeroMessagesProxy.ServerGetHeroSheet += ServerHandleGetHeroSheet;
        }

        private void OnDestroy() {
            DarklandHeroMessagesProxy.ServerGetHeroSheet -= ServerHandleGetHeroSheet;
        }

        [Server]
        private static void ServerHandleGetHeroSheet(NetworkConnectionToClient conn,
                                                     DarklandHeroMessages.GetHeroSheetRequestMessage message) {
            var identity = conn.identity;
            var heroVocation = identity.GetComponent<DarklandHero>().vocation;
            var heroLevel = identity.GetComponent<IXpHolder>().level;
            var heroName = identity.GetComponent<UnitNameBehaviour>().unitName;
            var heroTraits = identity.GetComponent<IStatsHolder>().TraitStatsValues();
            
            conn.Send(new DarklandHeroMessages.GetHeroSheetResponseMessage {
                heroVocation = heroVocation,
                heroLevel = heroLevel,
                heroName = heroName,
                heroTraits = heroTraits
            });
        }

    }

}