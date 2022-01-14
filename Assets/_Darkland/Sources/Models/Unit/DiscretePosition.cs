using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public class DiscretePosition {
        private readonly Emitable<Vector3Int> _position = new Emitable<Vector3Int>();

        public void Set(Vector3Int pos) {
            // _position.Set(pos);
        }

        // public Vector3Int Get() => _position.Get();
    }

}