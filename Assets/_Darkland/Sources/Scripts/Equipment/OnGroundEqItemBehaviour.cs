using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Equipment {

    public class OnGroundEqItemBehaviour : NetworkBehaviour, IOnGroundEqItem {

        private IMongoIdHolder _mongoIdHolder;
        private SpriteRenderer _spriteRenderer;

        public string ItemName { get; private set; }
        public ObjectId ItemMongoId => _mongoIdHolder.mongoId;
        public NetworkIdentity NetIdentity => netIdentity;

        private void Awake() {
            _mongoIdHolder = GetComponent<IMongoIdHolder>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        [Server]
        public void Init(string itemName, ObjectId itemMongoId) {
            ItemName = itemName;
            _mongoIdHolder.Set(itemMongoId);

            var eqItemDef = EqItemsContainer._.ItemDef2(itemName);
            _spriteRenderer.sprite = eqItemDef.Sprite;
        }
    }

}