using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Equipment {

    public interface IOnGroundEqItem {

        void Init(string itemName, EqItemType eqItemType, Sprite sprite);
        string ItemName { get; }
        EqItemType ItemType { get; }
        NetworkIdentity netIdentity { get; }

    }

}