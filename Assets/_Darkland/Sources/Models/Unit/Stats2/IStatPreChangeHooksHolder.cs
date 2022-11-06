using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatPreChangeHooksHolder {
        IEnumerable<IStatPreChangeHook> PreChangeHooks(StatId id);
    }

}