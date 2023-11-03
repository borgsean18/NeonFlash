using System;
using ProjectTime;
using UnityEngine;
using UnityEngine.Events;

namespace MainManagers
{
    public enum GameStates
    {
        Idle,
        Play,
        Pause,
        Lose,
        None
    }
    
    public abstract class GameManagerScript : MonoBehaviour
    {
        // Exposed Variables
        [Header("Events")]
        [SerializeField] private UnityEvent startGame;
        [SerializeField] private UnityEvent pauseGame;
        [SerializeField] private UnityEvent loseGame;
        
        [Header("Debug Settings")]
        [SerializeField] private bool immortalDebugRun;
        
        [Header("UI")]
        [SerializeField] private GameObject startGameButton;
        
        
        // Private Variables
        private GameStates gameState;
        
        
        // Properties
        public GameStates GameState => gameState;
        public bool ImmortalDebugRun => immortalDebugRun;
        public UnityEvent StartGame => startGame;
        public UnityEvent PauseGame => pauseGame;
        public UnityEvent LoseGame => loseGame;


        void Awake()
        {
            Init();
        }


        protected virtual void Init()
        {
            gameState = GameStates.Idle;
            
            TimeManager.Singleton.Init(this);
            
            // Add Lose method to the list of methods to be executed by the WorldManagers LoseGame event
            LoseGame.AddListener(Lose);
        }


        private void Update()
        {
            switch (gameState)
            {
                case GameStates.Idle:
                    // Wait for player to start the run
                    break;
                
                case GameStates.Play:
                    CalculateCurrentSpeed();
                    break;
                case GameStates.Pause:
                    
                    break;
                case GameStates.Lose:
                    SlowToHalt();
                    break;
                case GameStates.None:
                    break;
            }
        }


        public void PlayGame()
        {
            gameState = GameStates.Play;
            
            startGameButton.gameObject.SetActive(false);
            
            startGame.Invoke();
        }
        
        
        private void Lose()
        {
            if (immortalDebugRun)
                return;
            
            // Set Lose state
            gameState = GameStates.Lose;
            
            // Do score related things
        }


        protected abstract void CalculateCurrentSpeed();


        protected abstract void SlowToHalt();
    }
}
