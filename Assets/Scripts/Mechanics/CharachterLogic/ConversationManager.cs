﻿using Mechanics.CharacterLogic;
using TMPro;
using TMPro.SpriteAssetUtilities;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

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


        [SerializeField] private Sprite [] emojiSpriteSheet;

        [SerializeField] private float _timeProgressPerInteraction = 50;

        private TimeManager _timeManger;

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

            _timeManger = FindObjectOfType<TimeManager>();
            if (!_timeManger)
            {
                Debug.LogError("Missing TimeManager in scene");
            }

            currentConversation = conversations[currentConversationNum];
            _speechBubble.SpeakSprite(emojiSpriteSheet[Random.Range(0,emojiSpriteSheet.Length)]);

            SetConversations();
        }


        void SetConversations()
        {
            if (_speechOptions.buttons.Count == 0)
            {
                _speechOptions.initButtons();
            }
            
            /*
            int i = 0;
            foreach (string  str in currentConversation.ConvesationOptions)
            {
                _speechOptions.buttons[i].GetComponentInChildren<TextMeshProUGUI>().text = str;
                i++;
            }*/

            foreach (Button button in _speechOptions.buttons)
            {
                Image img = null;

                foreach (Image buttonimg in button.GetComponentsInChildren<Image>())
                {
                    if (buttonimg.name == "Emoji")
                    {
                        img = buttonimg;
                    }
                    
                    
                }

                if (img != null)
                {
                    img.sprite = emojiSpriteSheet[Random.Range(0, emojiSpriteSheet.Length)];
                } else  {
                    Debug.LogError("No Emoji IMG");
                        
                }

            }
        }
        // Update is called once per frame
        void Update()
        {
        
        }

        public void Converse(int i, string str = "")
        {
            _character.RelationshipIncrease(10);
            _timeManger.ProgressTime(_timeProgressPerInteraction);

            //_speechBubble.Speak(currentConversation.ConvesationResponses[i]);
            _speechBubble.SpeakSprite(emojiSpriteSheet[Random.Range(0,emojiSpriteSheet.Length)]);
            if (currentConversationNum < conversations.Length -1)
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
