using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqChangeServerListener {

        event Action<List<string>> ClientBackpackChanged;

    }

}