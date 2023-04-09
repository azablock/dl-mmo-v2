using System;
using Random = UnityEngine.Random;

namespace _Darkland.Sources.Models.Dice {

    public enum DiceType {

        D6,

    }

    public class DiceRoll {

        private int _val;
        
        public static DiceRoll Start() {
            return new DiceRoll();
        }

        public int Result() {
            _val = Math.Max(0, _val);
            return _val;
        }

        public DiceRoll D6(int amount = 1) => Dx(6, amount);

        public DiceRoll Modifier(int mod) {
            _val += mod;
            return this;
        }

        public DiceRoll Dx(int x, int amount = 1) {
            var result = 0;

            for (var i = 0; i < amount; i++) {
                result += Random.Range(0, x); //todo pytanie czy x + 1? (incl. vs excl.)
            }

            _val += result;

            return this;
        }

    }

}