using System;
using UnityEngine;

namespace Mechanics
{ 
    public class QuarantineManager : MonoBehaviour
    {

        private RoomsManager m_roomManager;

        private void Awake()
        {
            TimeManager timeManager = FindObjectOfType<TimeManager>(); 

            if (timeManager == null)
            {
                throw new System.Exception("Missing a TimeManager in the scene");
            }

            m_roomManager = FindObjectOfType<RoomsManager>();
            if (m_roomManager == null)
            {
                throw new System.Exception("Missing a RoomManager in the scene");
            }

            timeManager.OnTimePointReached += OnTimePointReached;
        }

        public void OnTimePointReached(EGameEffects gameEffects)
        {
            if ((gameEffects & EGameEffects.QUARANTINE_START) == EGameEffects.QUARANTINE_START)
            {
                StartQuarantine();
            }
            else if ((gameEffects & EGameEffects.QUARANTINE_END) == EGameEffects.QUARANTINE_END)
            {
                EndQuarantine();
            }
        }

        [ContextMenu("Start Quarantine")]
        public void StartQuarantine()
        {
            m_roomManager.VisitRoom(ERoomID.HOME);

            m_roomManager.OnRoomVisited += OnRoomVisited;
        }


        public void OnRoomVisited(ERoomID roomId, bool userInitiated)
        {
            foreach (QuarantineElement quarantined in GameObject.FindObjectsOfType<QuarantineElement>(true))
            {
                quarantined.gameObject.SetActive(true);
            }

            foreach (ReliefElement relieved in GameObject.FindObjectsOfType<ReliefElement>(false))
            {
                relieved.gameObject.SetActive(false);
            }

            m_roomManager.OnRoomVisited -= OnRoomVisited;
        }

        public void EndQuarantine()
        {

            foreach (QuarantineElement quarantined in GameObject.FindObjectsOfType<QuarantineElement>(false))
            {
                quarantined.gameObject.SetActive(false);
            }

            foreach (ReliefElement relieved in GameObject.FindObjectsOfType<ReliefElement>(true))
            {
                relieved.gameObject.SetActive(true);
            }

            m_roomManager.OnRoomVisited -= OnRoomVisited;
        }
    }
}