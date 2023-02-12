using UnityEngine;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqItemDef {

        string ItemName { get; }
        int ItemPrice { get; }
        EqItemType ItemType { get; }
        Sprite Sprite { get; }
        string Description(GameObject parent);

    }
    

}