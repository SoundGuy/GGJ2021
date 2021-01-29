using System;
using UnityEngine;

namespace Mechanics
{
    public class RoomsManager : MonoBehaviour
    {
        public Action<ERoomID> OnRoomVisited;
        public int RoomsVisited { get; private set; }

        public void UserVisitedRoom(ERoomID roomId)
        {
            RoomsVisited++;

            OnRoomVisited?.Invoke(roomId);
        }
    }

    public enum ERoomID
    {
        HOME,
        HOLOGRAMS,
        COFFEE,
        PARK,
        BEACH,
        CONFERENCE,
    }
}

