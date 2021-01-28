using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class TimePassage : MonoBehaviour
    {
        [SerializeField]
        private List<TimePoint> m_timePoints;

        [HideInInspector]
        public TimePoint ActiveTimePoint { get; set; }
    }

    public enum ETimeEffect
    {
        NONE,
        QUARANTINE_START,
        QUARANTINE_END,
    }

    [System.Serializable]
    public struct TimePoint
    {
        public string label;
        public ETimeEffect timeEffect;
    }
}