using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mechanics
{
    public class TimePassage : MonoBehaviour
    {
        [SerializeField]
        private float m_progressDurationSeconds;

        [SerializeField]
        private List<TimePoint> m_timePoints;

        [SerializeField]
        private int m_activeTimePointIndex = 0;

        private float m_elapsedTime;

        public TimePoint ActiveTimePoint
        {
            get
            {
                return m_timePoints[m_activeTimePointIndex];
            }
        }

        public void ProgressTime(float progressBy)
        {
            m_elapsedTime += progressBy;
        }

        private void Start()
        {
            m_elapsedTime = 0;
        }

        private void Update()
        {
            ProgressTime(Time.deltaTime);

            if ( m_elapsedTime >= ActiveTimePoint.activationTime )
            {
                if (m_activeTimePointIndex < m_timePoints.Count)
                {
                    m_activeTimePointIndex ++;
                }
            }
        }

        public void OrderTimePoints()
        {
            m_timePoints = m_timePoints.OrderBy(item => item.activationTime).ToList();
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
        public float activationTime;
    }
}