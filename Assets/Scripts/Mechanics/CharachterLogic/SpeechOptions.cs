using UnityEngine;
using UnityEngine.UIElements;

namespace Mechanics.CharachterLogic
{
    public class SpeechOptions : MonoBehaviour
    {
        [SerializeField] private Button[] buttons;
        [SerializeField] public ConversationManager _ConversationManager;
        // Start is called before the first frame update
        void Start()
        {
            buttons = GetComponentsInChildren<Button>();
            if (buttons == null || buttons.Length ==0)
            {
                Debug.LogError("No Buttons for " + this.name);
            }
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Converse(int c)
        {
            _ConversationManager.Converse(c); // TODO change this from int
        }
    }
}
