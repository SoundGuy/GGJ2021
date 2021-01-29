using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mechanics.CharachterLogic
{
    public class SpeechOptions : MonoBehaviour
    {
        [SerializeField] public List<Button> buttons;
        [SerializeField] public ConversationManager _ConversationManager;
        // Start is called before the first frame update
        void Start()
        {
            
            if (buttons.Count ==0)
            {
                initButtons();
            }
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Converse(int c)
        {
//            string str = buttons[c].GetComponentInChildren<TextMeshProUGUI>().text;
            _ConversationManager.Converse(c,""); // TODO change this from int
        }

        public void initButtons()
        {
            foreach (Button button in GetComponentsInChildren<Button>())
            {
                    buttons.Add(button);
            }
                
            if (buttons.Count ==0)
            {
                Debug.LogError("No Buttons for " + this.name);
            }
        }
    }
}
