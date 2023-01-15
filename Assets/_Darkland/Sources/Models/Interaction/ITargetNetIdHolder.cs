using System;
using Mirror;

namespace _Darkland.Sources.Models.Interaction {

    public interface ITargetNetIdHolder {

        void Set(uint newTargetNetId);
        void Clear();
        NetworkIdentity TargetNetIdentity { get; }
        event Action<NetworkIdentity> ServerChanged;
        event Action<NetworkIdentity> ServerCleared;

    }

    public interface ITargetNetIdClientNotifier {

        event Action<NetworkIdentity> ClientChanged;
        event Action<NetworkIdentity> ClientCleared;

    }

}