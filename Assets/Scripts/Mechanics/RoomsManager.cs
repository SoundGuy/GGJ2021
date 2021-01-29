using System;
using UnityEngine;

namespace Mechanics
{
    public class RoomsManager : MonoBehaviour
    {
        public int RoomsVisited { get; private set; }

        [SerializeField]
        private float secondsPassedPerRoom = 50f;

        [SerializeField]
        private TimeManager timePassage;

        public void UserVisitedRoom()
        {
            RoomsVisited++;

            timePassage.ProgressTime(secondsPassedPerRoom);
        }
    }
}

