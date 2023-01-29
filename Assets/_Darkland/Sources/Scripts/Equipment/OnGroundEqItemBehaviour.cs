using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Equipment {

    public class OnGroundEqItemBehaviour : NetworkBehaviour, IOnGroundEqItem {

        private SpriteRenderer _spriteRenderer;

        public ObjectId ObjectId { get; private set; }

        public string ItemName { get; private set; }
        public EqItemType ItemType { get; private set; }

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        [Server]
        public void Init(string itemName, EqItemType eqItemType, Sprite sprite) {
            ObjectId = ObjectId.GenerateNewId();
            ItemName = itemName;
            ItemType = ItemType;

            _spriteRenderer.sprite = sprite;
            
        }

    }

}