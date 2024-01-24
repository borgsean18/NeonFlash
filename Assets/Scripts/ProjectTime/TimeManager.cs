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
        private GameManagerScript gameManager;
        private float _timePassed;
        private bool _init;
        
        
        // Properties
        public bool IsInit => _init;
        public float TimePassed => _timePassed;


        void Awake()
        {
            Singleton = this;
        }


        public void Init(GameManagerScript _gameManagerScript)
        {
            gameManager = _gameManagerScript;
            
            _init = true;
        }


        private void Update()
        {
            if (_init && gameManager.GameState == GameStates.Play)
                _timePassed += Time.deltaTime;
        }
    }
}
