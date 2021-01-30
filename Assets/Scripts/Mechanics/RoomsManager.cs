using System;
using UnityEngine;

namespace Mechanics
{
    public class RoomsManager : MonoBehaviour
    {
        public Action<ERoomID> OnRoomVisited;
        public int RoomsVisited { get; private set; }

        [SerializeField]
        private string firstRoom = "Home";

        SceneLoadManager sceneLoadManager;

        public void UserVisitedRoom(ERoomID roomId)
        {
            RoomsVisited++;

            OnRoomVisited?.Invoke(roomId);
        }

        private void Awake()
        {
            sceneLoadManager = FindObjectOfType<SceneLoadManager>();

            if (sceneLoadManager == null)
            {
                throw new System.Exception("Missing a SceneLoadManager in the scene");
            }

            sceneLoadManager.ChangeScene(firstRoom);
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

