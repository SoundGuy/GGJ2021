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

            FindObjectOfType<TimeManager>().OnTimePointReached += OnTimePointReached;
        }

        public void OnTimePointReached(EGameEffects gameEffects)
        {
            if ((gameEffects & EGameEffects.QUARANTINE_START) == EGameEffects.QUARANTINE_START)
            {
                StartQuarantine();
            }
            else if ((gameEffects & EGameEffects.QUARANTINE_START) == EGameEffects.QUARANTINE_END)
            {
                EndQuarantine();
            }
        }

        public void StartQuarantine()
        {
            m_roomManager.VisitRoom(ERoomID.HOME);
        }

        public void EndQuarantine()
        {

        }
    }
}