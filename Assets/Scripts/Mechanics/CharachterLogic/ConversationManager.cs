using Mechanics.CharacterLogic;
using TMPro;
using UnityEngine;

namespace Mechanics.CharachterLogic
{
    
    public class ConversationManager : MonoBehaviour
    {
        [SerializeField] private SpeechBubble _speechBubble;
        [SerializeField] private SpeechOptions _speechOptions;

        [SerializeField] private ConversationSO currentConversation;
        [SerializeField] private int  currentConversationNum;
        [SerializeField] private ConversationSO[] conversations;

        [SerializeField] private Character _character;
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
            
            _character = GetComponentInChildren<Character>();
            if (!_character)
            {
                Debug.LogError("No Character");
            }

            currentConversation = conversations[currentConversationNum];

            //SetConversations();
        }


        void SetConversations()
        {
            int i = 0;
            foreach (string  str in currentConversation.ConvesationOptions)
            {
                _speechOptions.buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = str;
                i++;
            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        public void Converse(int i, string str = "")
        {
            _character.RelationshipIncrease(10);
            _speechBubble.Speak(currentConversation.ConvesationResponses[i]);
            if (currentConversationNum < conversations.Length)
            {
                currentConversation = conversations[++currentConversationNum];
                SetConversations();
            }
            else
            {
                // end of conversation
                _speechOptions.gameObject.SetActive(false); // TODO: is this really what's suppose to be happning?
            }
        }
    }
}
