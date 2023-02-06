using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
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
            DarklandHeroMessagesProxy.ServerGetEq += ServerHandleGetEq;
        }

        private void OnDestroy() {
            DarklandHeroMessagesProxy.ServerGetHeroSheet -= ServerHandleGetHeroSheet;
            DarklandHeroMessagesProxy.ServerGetEq -= ServerHandleGetEq;
        }

        [Server]
        private static void ServerHandleGetHeroSheet(NetworkConnectionToClient conn,
                                                     DarklandHeroMessages.GetHeroSheetRequestMessage message) {
            var identity = conn.identity;
            var heroVocation = identity.GetComponent<DarklandHeroBehaviour>().heroVocation.VocationType;
            var heroLevel = identity.GetComponent<IXpHolder>().level;
            var heroName = identity.GetComponent<UnitNameBehaviour>().unitName;
            var heroTraits = identity.GetComponent<IStatsHolder>().TraitStatsValues();
            var secondaryStats = identity.GetComponent<IStatsHolder>().SecondaryStatsValues();
            
            conn.Send(new DarklandHeroMessages.GetHeroSheetResponseMessage {
                heroVocationType = heroVocation,
                heroLevel = heroLevel,
                heroName = heroName,
                heroTraits = heroTraits,
                heroSecondaryStats = secondaryStats
            });
        }

        [Server]
        private static void ServerHandleGetEq(NetworkConnectionToClient conn,
                                              DarklandHeroMessages.GetEqRequestMessage message) {
            var eqHolder = conn.identity.GetComponent<IEqHolder>();
            var goldHolder = conn.identity.GetComponent<IGoldHolder>();
            var equippedWearables = new List<WearableDto>();
            
            foreach (var (wearableSlot, itemName) in eqHolder.EquippedWearables) {
                equippedWearables.Add(new WearableDto {
                    wearableSlot = wearableSlot,
                    itemName = itemName
                });
            }

            conn.Send(new DarklandHeroMessages.GetEqResponseMessage {
                itemNames = eqHolder.Backpack.Select(it => it.ItemName).ToList(),
                equippedWearables = equippedWearables,
                goldAmount = goldHolder.GoldAmount
            });
        }

    }

}