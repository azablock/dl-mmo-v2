using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IStatsCounter {

        Dictionary<StatId, float> Count(GameObject gameObject);

    }
    
    public class StatsCalculatorBehaviour : MonoBehaviour {

        

    }
}