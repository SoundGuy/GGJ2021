using TMPro;
using UnityEngine;

namespace Mechanics.CharachterLogic
{
    public class SpeechBubble : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speech; // TODO change to emoji ?
        // Start is called before the first frame update

        void Speak(string phrase)
        {
            speech.text = phrase;
        }
            
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
