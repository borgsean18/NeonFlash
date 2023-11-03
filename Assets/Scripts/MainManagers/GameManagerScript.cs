using System;
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
    
    public class GameManagerScript : MonoBehaviour
    {
        // Exposed Variables
        [Header("Events")]
        [SerializeField] private UnityEvent startGame;
        [SerializeField] private UnityEvent pauseGame;
        [SerializeField] private UnityEvent loseGame;
        
        
        // Private Variables
        private GameStates gameState;
        private WorldManager worldManager;
        
        
        // Properties
        public GameStates GameState => gameState;
        public UnityEvent StartGame => startGame;
        public UnityEvent PauseGame => pauseGame;
        public UnityEvent LoseGame => loseGame;


        void Awake()
        {
            Init();
        }


        private void Init()
        {
            worldManager = GetComponent<WorldManager>();

            gameState = GameStates.Idle;
        }
    }
}
