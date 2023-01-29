using System;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Persistence.Entity;
using _Darkland.Sources.Scripts.Persistence;
using Castle.Core.Internal;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class OnGroundItemsManager : MonoBehaviour {

        public static OnGroundItemsManager _;

        [SerializeField]
        private GameObject onGroundItemPrefab;

        private void Awake() {
            _ = this;
            DarklandNetworkManager.ServerStarted += ServerLoadAndSpawnAll;
        }

        private void OnDestroy() {
            DarklandNetworkManager.ServerStarted -= ServerLoadAndSpawnAll;
        }

        [Server]
        public void ServerCreateOnGroundItem(Vector3Int pos, string itemName) {
            Assert.IsFalse(itemName.IsNullOrEmpty());

            var entity = new OnGroundEqItemEntity {
                itemName = itemName,
                createDate = DateTime.Now,
                posX = pos.x,
                posY = pos.y,
                posZ = pos.z
            };

            DarklandDatabaseManager.onGroundEqItemRepository.Create(entity);

            var instance = Instantiate(onGroundItemPrefab, pos, Quaternion.identity);
            instance.GetComponent<IOnGroundEqItem>().Init(itemName, entity.id);

            NetworkServer.Spawn(instance);
        }

        [Server]
        public void ServerDestroyOnGroundItem(IOnGroundEqItem item) {
            Assert.IsNotNull(item?.NetIdentity);

            NetworkServer.Destroy(item.NetIdentity.gameObject);
            DarklandDatabaseManager.onGroundEqItemRepository.Delete(item.ItemMongoId);
        }

        [Server]
        private void ServerLoadAndSpawnAll() {
            DarklandDatabaseManager
                .onGroundEqItemRepository
                .FindAll()
                .ToList()
                .ForEach(ServerLoadItemFromDb);
        }

        [Server]
        private void ServerLoadItemFromDb(OnGroundEqItemEntity entity) {
            var pos = new Vector3Int(entity.posX, entity.posY, entity.posZ);
            var itemName = entity.itemName;
            var instance = Instantiate(onGroundItemPrefab, pos, Quaternion.identity);
    
            instance.name = $"On Ground Eq Item [{itemName} {pos}]";
            instance.GetComponent<IOnGroundEqItem>().Init(itemName, entity.id);
            NetworkServer.Spawn(instance);
        }

    }

}