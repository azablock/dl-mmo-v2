using _Darkland.Sources.Models.Equipment;
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
        }

        [Server]
        public void ServerSpawnOnGroundItem(Vector3Int pos, string itemName, EqItemType itemType, Sprite sprite) {
            Assert.IsFalse(itemName.IsNullOrEmpty());
            Assert.IsNotNull(sprite);
            
            var instance = Instantiate(onGroundItemPrefab, pos, Quaternion.identity);
            instance.GetComponent<IOnGroundEqItem>().Init(itemName, itemType, sprite);
            
            //todo db save on ground item
            
            NetworkServer.Spawn(instance);
        }

        [Server]
        public void ServerDestroyOnGroundItem(IOnGroundEqItem item) {
            Assert.IsNotNull(item?.netIdentity);
            
            NetworkServer.Destroy(item.netIdentity.gameObject);
        }
    }

}