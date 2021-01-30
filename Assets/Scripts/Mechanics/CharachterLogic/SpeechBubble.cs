using TMPro;
using UnityEngine;

namespace Mechanics.CharachterLogic
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speech; // TODO change to emoji ?
        [SerializeField] private UnityEngine.UI.Image emoji; // TODO change to emoji ?
        // Start is called before the first frame update

        public void Speak(string phrase)
        {
            speech.text = phrase;
        }
            
        void Awake()
        {
            /*speech = GetComponentInChildren<TextMeshProUGUI>();
            if (!speech)
            {
                Debug.LogError("No speech");
            }*/
            
            
            
          foreach (UnityEngine.UI.Image buttonimg in GetComponentsInChildren<UnityEngine.UI.Image>())
            {
                if (buttonimg.name == "Emoji")
                {
                    emoji = buttonimg;
                }
                
                
            }
            
            //emoji = GetComponentInChildren<UnityEngine.UI.Image>();
            if (!emoji)
            {
                Debug.LogError("No emoji");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void SpeakSprite(Sprite sprite)
        {
            emoji.sprite = sprite;
        }
    }
}
