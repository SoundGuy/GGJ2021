using UnityEngine;

namespace Mechanics.CharachterLogic
{
    [CreateAssetMenu(menuName = "Characters/Conversation")]
    public class ConversationSO : ScriptableObject
    {
        [SerializeField] public string [] ConvesationOptions;
        [SerializeField] public string [] ConvesationResponses;
        
    }
}
