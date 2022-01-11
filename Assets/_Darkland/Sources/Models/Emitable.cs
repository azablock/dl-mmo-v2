using System;

namespace _Darkland.Sources.Models {

    public class Emitable<T> {
        
        public Action<T> Changed;

        private T _value;
        
        public void Set(T newValue) {
            _value = newValue;
            Changed?.Invoke(_value);
        }

        public T Get() => _value;
    }

}