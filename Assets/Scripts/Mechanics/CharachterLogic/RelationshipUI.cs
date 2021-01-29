using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Mechanics.CharacterLogic
{
    public class RelationshipUI : MonoBehaviour
    {
        [SerializeField] private TextMeshPro indicator;

        public void UpdateIndicator(float value)
        {
            indicator.text = value.ToString("00");
        }

        // Start is called before the first frame update
        void Start()
        {
            if (!indicator)
            {
                indicator = GetComponentInChildren<TextMeshPro>();
                if (!indicator)
                {
                    Debug.LogError("No Indicator!");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
