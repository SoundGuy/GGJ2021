using System;
using UnityEngine;

namespace Mechanics
{
    public class RoomsManager : MonoBehaviour
    {
        public Action<ERoomID> OnRoomVisited;
        public int RoomsVisited { get; private set; }

        [SerializeField]
        private ERoomID firstRoom = ERoomID.HOME;

        SceneLoadManager sceneLoadManager;

        public void VisitRoom(ERoomID roomId)
        {
            RoomsVisited++;

            OnRoomVisited?.Invoke(roomId);

            ChangeRoomById(roomId);
        }

        private void ChangeRoomById(ERoomID roomId)
        {
            switch (roomId)
            {
                case ERoomID.HOME:
                    sceneLoadManager.ChangeScene("Home");
                    break;
                case ERoomID.COFFEE:
                    sceneLoadManager.ChangeScene("CoffeeShop");
                    break;
                case ERoomID.PARK:
                    sceneLoadManager.ChangeScene("Park");
                    break;
                case ERoomID.BEACH:
                    sceneLoadManager.ChangeScene("Beach");
                    break;
                case ERoomID.CONFERENCE:
                    sceneLoadManager.ChangeScene("ConferenceRoom");
                    break;
            }
        }

        private void Awake()
        {
            sceneLoadManager = FindObjectOfType<SceneLoadManager>();

            if (sceneLoadManager == null)
            {
                throw new System.Exception("Missing a SceneLoadManager in the scene");
            }

            ChangeRoomById(firstRoom);
        }

        [ContextMenu("Visit Park")]
        public void VisitPark()
        {
            VisitRoom(ERoomID.PARK);
        }

        [ContextMenu("Visit Conference Room")]
        public void VisitConferenceRoom()
        {
            VisitRoom(ERoomID.CONFERENCE);
        }
    }

    public enum ERoomID
    {
        HOME,
        COFFEE,
        PARK,
        BEACH,
        CONFERENCE,
    }
}