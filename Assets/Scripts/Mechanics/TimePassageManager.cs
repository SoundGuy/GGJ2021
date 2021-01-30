using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{ 
    public class TimePassageManager : MonoBehaviour
    {
        private TimeManager timeManager;

        public void Awake()
        {
            timeManager = FindObjectOfType<TimeManager>();

            if (timeManager == null)
            {
                throw new System.Exception("Missing TimeManager in the scene");
            }
        }

        private void Update()
        {
            timeManager.ProgressTime(Time.deltaTime);
        }
    }
}