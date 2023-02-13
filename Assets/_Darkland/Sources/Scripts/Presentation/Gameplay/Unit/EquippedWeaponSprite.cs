using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class EquippedWeaponSprite : MonoBehaviour {

        [SerializeField]
        private EqChangeServerListenerBehaviour eqChangeServerListener;
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            eqChangeServerListener.ClientWearableEquipped += ClientOnWearableEquipped;
            eqChangeServerListener.ClientWearableCleared += ClientOnWearableCleared;
        }

        private void OnDestroy() {
            eqChangeServerListener.ClientWearableEquipped -= ClientOnWearableEquipped;
            eqChangeServerListener.ClientWearableCleared -= ClientOnWearableCleared;
        }

        [Client]
        private void ClientOnWearableEquipped(WearableSlot wearableSlot, string itemName) {
            if (wearableSlot != WearableSlot.RightHand) return;
            spriteRenderer.sprite = EqItemsContainer.ItemDef2(itemName).Sprite;
        }

        [Client]
        private void ClientOnWearableCleared(WearableSlot wearableSlot) {
            if (wearableSlot == WearableSlot.RightHand) {
                spriteRenderer.sprite = null;
            }
        }

    }

}