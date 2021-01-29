using UnityEngine;

namespace Mechanics
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private float relationshipStrength;
        [SerializeField] private RelationshipUI _relationshipUIIndicator;
        
        // TODO: add visual healthbar object to update.
        
        public float RelationshipStrength
        {
            get => relationshipStrength;
            set => relationshipStrength = value;
        }

        public void RelationshipDecay(float amount)
        {
            relationshipStrength -= amount;
            if (relationshipStrength < 0)
            {
                relationshipStrength = 0;
                // todo call an action when reached 0.
            }
        }
        
        
        public void RelationshipIncrease(float amount)
        {
            relationshipStrength -= amount;
            if (relationshipStrength > 100f) // TODO : move to some sort of parameter settings.
            {
                relationshipStrength = 100f;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _relationshipUIIndicator = GetComponentInChildren<RelationshipUI>();
            if (!_relationshipUIIndicator)
            {
                Debug.LogError("No RelationshipUI");
            }
        }

        // Update is called once per frame
        void Update()
        {
            _relationshipUIIndicator.UpdateIndicator(relationshipStrength); // TODO only call  on change and not up update
        }
    }
}
