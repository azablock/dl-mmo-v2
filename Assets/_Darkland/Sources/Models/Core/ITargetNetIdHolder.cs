using System;
using _Darkland.Sources.Scripts;
using _Darkland.Sources.Scripts.Ai;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Core {

    public interface ITargetNetIdHolder {

        void Set(uint newTargetNetId);
        void Clear();
        NetworkIdentity TargetNetIdentity { get; }
        float MaxTargetDistance { get; }
        event Action<NetworkIdentity> ServerChanged;
        event Action<NetworkIdentity> ServerCleared;
        
    }

    public static class TargetNetIdHolderExtensions {

        [Server]
        public static bool IsTargetEnemy(this ITargetNetIdHolder holder) {
            return HasTarget(holder) && holder.TargetNetIdentity.GetComponent<DarklandMobBehaviour>() != null;
        }
        
        [Server]
        public static bool IsTargetFriendly(this ITargetNetIdHolder holder) {
            return HasTarget(holder) && holder.TargetNetIdentity.GetComponent<DarklandHeroBehaviour>() != null;
        }

        [Server]
        public static bool HasTarget(this ITargetNetIdHolder holder) => holder.TargetNetIdentity != null;

        [Server]
        public static Vector3Int TargetPos(this ITargetNetIdHolder holder) =>
            holder.TargetNetIdentity.GetComponent<IDiscretePosition>().Pos;

    }

    public interface ITargetNetIdClientNotifier {

        event Action<NetworkIdentity> ClientChanged;
        event Action<NetworkIdentity> ClientCleared;

    }

}