using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public enum AiPerceptionZoneType {
        Passive,
        Attack
    }

    public class AiNetworkPerceptionZone {

        public readonly float range;
        public readonly HashSet<NetworkIdentity> targets;
        public event Action<NetworkIdentity> TargetEnteredZone;
        public event Action<NetworkIdentity> TargetExitedZone;

        public AiNetworkPerceptionZone(float range) {
            this.range = range;
            targets = new();
        }

        public void OnTargetEnter(NetworkIdentity target) {
            targets.Add(target);
            TargetEnteredZone?.Invoke(target);
            // Debug.Log($"OnTargetEnter {target.name} netId={target.netId}\t{NetworkTime.time}");
        }

        public void OnTargetExit(NetworkIdentity target) {
            targets.Remove(target);
            TargetExitedZone?.Invoke(target);
            // Debug.Log($"OnTargetExit {target.name} netId={target.netId}\t{NetworkTime.time}");
        }

    }
    
    public interface IAiNetworkPerception {
        float PerceptionZoneRange(AiPerceptionZoneType zoneType);

        Dictionary<AiPerceptionZoneType, AiNetworkPerceptionZone> PerceptionZones { get; }
    }

}