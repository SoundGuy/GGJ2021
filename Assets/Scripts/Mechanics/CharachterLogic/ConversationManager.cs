using UnityEngine;

namespace Mechanics.CharachterLogic
{
    
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] private SpeechBubble _speechBubble;
        [SerializeField] private SpeechOptions _speechOptions;
        // Start is called before the first frame update
        void Start()
        {
            _speechBubble = GetComponentInChildren<SpeechBubble>();
            if (!_speechBubble)
            {
                Debug.LogError("No _speechBubble");
            }
            _speechOptions = GetComponentInChildren<SpeechOptions>();
            if (!_speechOptions)
            {
                Debug.LogError("No _speechBubble");
            }
            else
            {
                _speechOptions._ConversationManager = this;
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Converse(int i)
        {
            throw new System.NotImplementedException();
        }
    }
}
