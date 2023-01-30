using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Equipment {

    public class OnGroundEqItemBehaviour : NetworkBehaviour, IOnGroundEqItem {

        private IDiscretePosition _discretePosition;
        private IMongoIdHolder _mongoIdHolder;
        private SpriteRenderer _spriteRenderer;

        [field: SyncVar(hook = nameof(ClientSyncItemName))]
        public string ItemName { get; private set; }
        public ObjectId ItemMongoId => _mongoIdHolder.mongoId;
        public NetworkIdentity NetIdentity => netIdentity;
        public Vector3Int Pos => _discretePosition.Pos;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
            _mongoIdHolder = GetComponent<IMongoIdHolder>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        [Server]
        public void Init(string itemName, ObjectId itemMongoId, Vector3Int discretePos) {
            ItemName = itemName;
            _mongoIdHolder.Set(itemMongoId);
            _discretePosition.Set(discretePos);

            SetItemSprite(itemName);
        }

        [Client]
        private void ClientSyncItemName(string _, string val) => SetItemSprite(val);

        private void SetItemSprite(string itemName) {
            var eqItemDef = EqItemsContainer.ItemDef2(itemName);
            _spriteRenderer.sprite = eqItemDef.Sprite;
        }
    }

}