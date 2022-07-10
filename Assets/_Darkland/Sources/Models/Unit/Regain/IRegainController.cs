namespace _Darkland.Sources.Models.Unit.Regain {

    public interface IRegainController {
        float GetRegain(float regainRate);
    }

    public class RegainController : IRegainController {

        private float _regainValue;

        public float GetRegain(float regainRate) {
            _regainValue += regainRate;
            var regainValueInt = (int) _regainValue;
            _regainValue -= regainValueInt;

            return regainValueInt;
        }
    }

}