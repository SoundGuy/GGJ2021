using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{ 
    public class TimePassageManager : MonoBehaviour
    {
        [SerializeField]
        private float timeScale = 1f;

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
            timeManager.ProgressTime(Time.deltaTime * timeScale);
        }
    }
}