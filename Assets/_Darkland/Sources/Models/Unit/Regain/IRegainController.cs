namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IRegainController {
        float regainValue { get; }
        float GetRegain(float regainRate);
    }

    public class RegainController : IRegainController {

        public float regainValue { get; }

        public float GetRegain(float regainRate) {
            throw new System.NotImplementedException();
        }
    }

}