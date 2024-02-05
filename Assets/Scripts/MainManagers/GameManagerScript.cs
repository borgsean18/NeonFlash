using System.Collections;
using ProjectTime;
using SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace MainManagers
{
    public enum GameStates
    {
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
        [SerializeField] private UnityEvent resumeGame;
        [SerializeField] private UnityEvent pauseGame;
        [SerializeField] private UnityEvent loseGame;
        
        
        // Private Variables
        private GameStates gameState;
        private UIManager uIManager;
        private SceneFaderManager sceneFaderManager;
        
        
        // Properties
        public GameStates GameState => gameState;
        public UnityEvent StartGame => startGame;
        public UnityEvent PauseGame => pauseGame;
        public UnityEvent LoseGame => loseGame;


        void Awake()
        {
            Init();
        }


        protected virtual void Init()
        {
            gameState = GameStates.Pause;
            
            TimeManager.Singleton.Init(this);

            uIManager = GetComponent<UIManager>();
            uIManager.Init(this);

            sceneFaderManager = FindObjectOfType<SceneFaderManager>();
            
            // Add Lose method to the list of methods to be executed by the WorldManagers LoseGame event
            LoseGame.AddListener(Lose);
        }


        private void Update()
        {
            switch (gameState)
            {
                case GameStates.Play:
                    CalculateCurrentSpeed();
                    break;
                case GameStates.Pause:
                    // Pause methods
                    break;
                case GameStates.Lose:
                    SlowToHalt();
                    break;
                case GameStates.None:
                    break;
            }
        }


        /// <summary>
        /// This method is called in Unity on the Start Button in the Canvas
        /// </summary>
        public void Play()
        {
            gameState = GameStates.Play;
            
            startGame.Invoke();
        }


        /// <summary>
        /// Not used anywhere yet. Will run ONCE when a Resume button is pressed
        /// </summary>
        public void Resume()
        {
            resumeGame.Invoke();
        }


        /// <summary>
        /// Not used anywhere yet. Will run ONCE when a Pause button is pressed
        /// </summary>
        public void Pause()
        {
            pauseGame.Invoke();
        }
        
        
        private void Lose()
        {
            // Set Lose state
            gameState = GameStates.Lose;
            
            // Do score related things
            // ---
        }


        public void Retry()
        {
            if (sceneFaderManager == null)
                sceneFaderManager = FindObjectOfType<SceneFaderManager>();

            // If null at this point then probably just playing through UnityEditor
            if (sceneFaderManager == null)
                return;

            string sceneName = SceneManager.GetActiveScene().name;

            sceneFaderManager.TransitionToScene(sceneName);
        }


        protected abstract void CalculateCurrentSpeed();


        protected abstract void SlowToHalt();
    }
}
