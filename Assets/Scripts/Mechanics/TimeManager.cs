using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Mechanics
{
    public class TimeManager : MonoBehaviour
    {
        public Action<EGameEffects> OnTimePointReached;

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

        private RoomsManager m_roomsManager;

        public float ElapsedTime { get; private set; }

        public void ProgressTime(float progressBy)
        {
            bool shouldCheckMaxRoomTime = (m_maxTimePerRoomVisits.Count > 0 && m_roomsManager.RoomsVisited < m_maxTimePerRoomVisits.Count);
            if (shouldCheckMaxRoomTime)
            {
                int currentRoomMaxTime = m_maxTimePerRoomVisits[m_roomsManager.RoomsVisited];

                if (ElapsedTime <= currentRoomMaxTime)
                {
                    ElapsedTime += progressBy;
                }
            }
            else
            {
                ElapsedTime += progressBy;
            }

            bool shouldStopPointProgress = ( m_timePoints.Count == 0 || (m_activeTimePointIndex + 1 >= m_timePoints.Count) );

            if (shouldStopPointProgress) return;

            TimePoint nextTimePoint = m_timePoints[m_activeTimePointIndex + 1];
            if (m_activeTimePointIndex < m_timePoints.Count &&
                ElapsedTime >= nextTimePoint.activationTime)
            {
                {
                    m_activeTimePointIndex++;

                    TimePoint activeTimePoint = m_timePoints[m_activeTimePointIndex];
                    OnTimePointReached(activeTimePoint.timeEffects);
                }
            }
        }

        private void Awake()
        {
            ElapsedTime = 0;
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

    [System.Serializable]
    public struct TimePoint
    {
        public string label;
        public EGameEffects timeEffects;
        public float activationTime;
    }
}