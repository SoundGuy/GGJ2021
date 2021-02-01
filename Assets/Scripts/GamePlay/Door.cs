using UnityEngine;
using Mechanics;

public class Door : MonoBehaviour
{
    [SerializeField]
    private GameObject m_doorLock;

    [SerializeField]
    private ERoomID m_destinationRoomId;

    private RoomsManager m_roomsManager;

    public void OpenDoor()
    {
        // Check if we can get out or not.
        if (m_doorLock.activeSelf == false)
        {
            m_roomsManager.VisitRoom(m_destinationRoomId, true);
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        m_roomsManager = FindObjectOfType<RoomsManager>();

        if (m_roomsManager == null)
        {
            throw new System.Exception("Missing component of type RoomsManager in the scene");
        }
    }
}
