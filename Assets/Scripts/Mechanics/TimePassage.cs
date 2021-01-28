using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
    public class TimePassage : MonoBehaviour
    {
        [SerializeField]
        private float m_progressDurationSeconds;

        [SerializeField]
        private List<TimePoint> m_timePoints;

        private int m_activeTimePointIndex;

        private float m_lastPointTime;

        public TimePoint ActiveTimePoint
        {
            get
            {
                return m_timePoints[m_activeTimePointIndex];
            }
        }

        public void ProgressTime(int progressBy)
        {
            m_activeTimePointIndex += progressBy;
        }

        private void Start()
        {
            m_lastPointTime = Time.time;
        }

        private void Update()
        {
            if( Time.time - m_lastPointTime > m_progressDurationSeconds)
            {
                m_lastPointTime = Time.time;

                if (m_activeTimePointIndex < m_timePoints.Count)
                {
                    ProgressTime(1);
                }
            }
        }
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