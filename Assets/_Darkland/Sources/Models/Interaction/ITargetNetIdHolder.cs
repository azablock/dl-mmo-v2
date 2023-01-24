using System;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Scripts;
using _Darkland.Sources.Scripts.Bot;
using Mirror;

namespace _Darkland.Sources.Models.Interaction {

    public interface ITargetNetIdHolder {

        void Set(uint newTargetNetId);
        void Clear();
        NetworkIdentity TargetNetIdentity { get; }
        event Action<NetworkIdentity> ServerChanged;
        event Action<NetworkIdentity> ServerCleared;

    }

    public static class TargetNetIdHolderExtensions {

        [Server]
        public static bool IsTargetEnemy(this ITargetNetIdHolder holder) {
            return HasTarget(holder) && holder.TargetNetIdentity.GetComponent<DarklandBot>() != null;
        }
        
        [Server]
        public static bool IsTargetFriendly(this ITargetNetIdHolder holder) {
            return HasTarget(holder) && holder.TargetNetIdentity.GetComponent<DarklandHero>() != null;
        }

        [Server]
        public static bool HasTarget(this ITargetNetIdHolder holder) => holder.TargetNetIdentity != null;

    }

    public interface ITargetNetIdClientNotifier {

        event Action<NetworkIdentity> ClientChanged;
        event Action<NetworkIdentity> ClientCleared;

    }

}