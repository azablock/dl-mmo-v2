using UnityEngine;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqItemDef {

        public string ItemName { get; }
        public int ItemPrice { get; }
        public EqItemType ItemType { get; }
        public Sprite Sprite { get; }

    }

}