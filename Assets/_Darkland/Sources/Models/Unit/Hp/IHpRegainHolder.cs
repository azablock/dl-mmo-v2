namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpRegainHolder {
        float HpRegain { get; }
        void IncrementHpRegain();
        int ResolveHpToRegain();
    }

    public class HpRegainHolder : IHpRegainHolder {
        private float _hpRegainSum;

        public float HpRegain => 1.0f;

        public void IncrementHpRegain() {
            _hpRegainSum += HpRegain;
        }

        public int ResolveHpToRegain() {
            var hpToRegain = (int) _hpRegainSum;
            _hpRegainSum -= hpToRegain;

            return hpToRegain;
        }
    }

}