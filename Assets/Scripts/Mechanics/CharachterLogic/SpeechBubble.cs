using TMPro;
using UnityEngine;

namespace Mechanics.CharachterLogic
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speech; // TODO change to emoji ?
        // Start is called before the first frame update

        public void Speak(string phrase)
        {
            speech.text = phrase;
        }
            
        void Start()
        {
            speech = GetComponentInChildren<TextMeshProUGUI>();
            if (!speech)
            {
                Debug.LogError("No speech");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
