using System;

namespace _Darkland.Sources.Models {

    public class Emitable<T> {

        public event Action<T> Changed;
        private T _value;

        public T Value {
            get => _value;
            set {
                _value = value;
                Changed?.Invoke(_value);
            }
        }

    }

}