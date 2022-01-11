using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public class UnitHpActions {
        public Action<int> serverHpChanged;
        public Action<int> serverMaxHpChanged;
        public Action<int> clientHpChanged;
        public Action<int> clientMaxHpChanged;
    }

}