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

        private ERoomID lastRoomVisited;

        private SceneLoadManager sceneLoadManager;

        public void VisitRoom(ERoomID roomId)
        {
            if (lastRoomVisited == roomId)
            {
                RoomVisited(roomId);
            }

            lastRoomVisited = roomId;

            ChangeRoomById(roomId);
            sceneLoadManager.OnNewSceneActive += () => { RoomVisited(roomId); };
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

        private void RoomVisited(ERoomID roomId)
        {
            RoomsVisited++;

            OnRoomVisited?.Invoke(roomId);
        }

        private void Awake()
        {
            lastRoomVisited = firstRoom;

            sceneLoadManager = FindObjectOfType<SceneLoadManager>();

            if (sceneLoadManager == null)
            {
                throw new System.Exception("Missing a SceneLoadManager in the scene");
            }
#if UNITY_EDITOR
            if (UnityEngine.SceneManagement.SceneManager.sceneCount > 1)
            {
              // TODO: check if the other opened scene is actually a room.
            }
            else
            {
              ChangeRoomById(firstRoom);
            }
#else
            ChangeRoomById(firstRoom);
#endif

            FlatController flatController = FindObjectOfType<FlatController>();

            OnRoomVisited += (ERoomID) => { flatController.UpdateCamera(); };
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