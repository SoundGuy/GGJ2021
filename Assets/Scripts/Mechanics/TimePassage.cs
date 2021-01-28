using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class TimePassage : MonoBehaviour
    {
        [SerializeField]
        private List<TimePoint> m_timePoints;
    }

    enum ETimeEffect
    {
        NONE,
        QUARANTINE_START,
        QUARANTINE_END,        
    }

    [System.Serializable]
    struct TimePoint
    {
        public string label;
        public ETimeEffect timeEffect;
    }
}