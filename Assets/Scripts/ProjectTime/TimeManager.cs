using System;
using UnityEngine;

namespace ProjectTime
{
    public class TimeManager : MonoBehaviour
    {
        // Singleton
        public static TimeManager Singleton;

        // Private Variables
        private float _timePassed;
        private bool _paused;

        // Properties
        public float TimePassed => _timePassed;

        
        private void Awake()
        {
            Singleton = this;
        }


        private void Update()
        {
            if (!_paused)
                _timePassed += Time.deltaTime;
        }
    }
}
