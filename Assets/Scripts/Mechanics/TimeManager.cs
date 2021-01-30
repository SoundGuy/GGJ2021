using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mechanics
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField]
        private float m_progressTimePerRoomVisit = 50f;

        [SerializeField]
        private float m_progressDurationSeconds;

        [SerializeField]
        private List<TimePoint> m_timePoints;

        [SerializeField]
        private List<int> m_maxTimePerRoomVisits;

        [SerializeField]
        private int m_activeTimePointIndex;

        private float m_elapsedTime;

        private RoomsManager m_roomsManager;

        public TimePoint ActiveTimePoint
        {
            get
            {
                return m_timePoints[m_activeTimePointIndex];
            }
        }

        public void ProgressTime(float progressBy)
        {
            bool shouldCheckMaxRoomTime = (m_maxTimePerRoomVisits.Count > 0 && m_roomsManager.RoomsVisited < m_maxTimePerRoomVisits.Count);
            if (shouldCheckMaxRoomTime)
            {
                int currentRoomMaxTime = m_maxTimePerRoomVisits[m_roomsManager.RoomsVisited];

                if (m_elapsedTime <= currentRoomMaxTime)
                {
                    m_elapsedTime += progressBy;
                }
            }
            else
            {
                m_elapsedTime += progressBy;
            }

            bool shouldStopPointProgress = ( m_timePoints.Count == 0 || (m_activeTimePointIndex + 1 >= m_timePoints.Count) );

            if (shouldStopPointProgress) return;

            TimePoint nextTimePoint = m_timePoints[m_activeTimePointIndex + 1];
            if (m_activeTimePointIndex < m_timePoints.Count &&
                m_elapsedTime >= nextTimePoint.activationTime)
            {
                {
                    m_activeTimePointIndex++;
                }
            }
        }

        private void Awake()
        {
            m_elapsedTime = 0;
            m_activeTimePointIndex = -1;
            m_roomsManager = FindObjectOfType<RoomsManager>();

            if (m_roomsManager == null)
            {
                throw new System.Exception("Missing RoomsManager in the scene");
            }

            m_roomsManager.OnRoomVisited += OnRoomVisited;
        }

        private void OnRoomVisited(ERoomID roomId)
        {
            ProgressTime(m_progressTimePerRoomVisit);
        }

        public void OrderTimePoints()
        {
            if (m_timePoints.Count > 0)
            {
                m_timePoints = m_timePoints.OrderBy(item => item.activationTime).ToList();
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
        public float activationTime;
    }
}