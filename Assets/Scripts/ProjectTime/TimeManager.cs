using System;
using MainManagers;
using UnityEngine;

namespace ProjectTime
{
    public class TimeManager : MonoBehaviour
    {
        // Singleton
        public static TimeManager Singleton;

        // Private Variables
        private WorldManager worldManager;
        private float _timePassed;
        
        // Properties
        public float TimePassed => _timePassed;

        public WorldManager WorldManager
        {
            get => worldManager;
            set => worldManager = value;
        }


        private void Awake()
        {
            Singleton = this;
        }


        private void Update()
        {
            if (worldManager.GameState == GameStates.Play)
                _timePassed += Time.deltaTime;
        }
    }
}
