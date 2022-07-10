using System;

namespace _Darkland.Sources.Models.World {

    public interface IGameWorldTime {
        TimeSpan now { get; }
    }

}