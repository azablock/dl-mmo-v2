namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpHolder {
        int hp { get; set; }
        int maxHp { get; set; }
    }

    public class HpHolder : IHpHolder {
        public int hp { get; set; }
        public int maxHp { get; set; }
    }

}