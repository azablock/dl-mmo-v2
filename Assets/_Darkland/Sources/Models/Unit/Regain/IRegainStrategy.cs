using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Regain {

    public interface IRegainStrategy {
        float Get(GameObject go);
    }

}