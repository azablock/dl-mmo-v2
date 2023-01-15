using System;
using Mirror;

namespace _Darkland.Sources.Models.Interaction {
    
    public interface ITargetNetIdHolder {
        NetworkIdentity targetNetIdentity { get; }
        event Action<NetworkIdentity> ServerChanged;
        event Action<NetworkIdentity> ClientChanged;
        event Action<NetworkIdentity> ClientCleared;
    }

}