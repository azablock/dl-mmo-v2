using _Darkland.Sources.Models.Unit.Regain;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.RegainStrategy {

    public abstract class RegainStrategy : ScriptableObject, IRegainStrategy {
        public abstract float Get(GameObject go);
    }

}