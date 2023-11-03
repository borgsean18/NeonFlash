using System.Collections.Generic;
using ProjectTime;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using World;
using Random = UnityEngine.Random;

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
    
    public class WorldManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("General Settings")]
        [SerializeField] private GameStates gameState;
        [SerializeField] private Vector3 worldInitPoint;
        
        [Header("Debug Settings")]
        [SerializeField] private bool immortalDebugRun;
        
        [Header("Biome Settings")]
        [SerializeField, Min(150)] private int minSegmentsPerBiome;
        [SerializeField, Min(250)] private int maxSegmentsPerBiome;
        [SerializeField] private List<GameObject> biomePrefabs;
        
        [Header("World Movement")]
        [SerializeField] protected float maxSpeed;
        [SerializeField] private AnimationCurve speedMultiplier;
        [SerializeField] private float minutesTillMaxSpeed;
        
        [Header("UI")]
        [SerializeField] private GameObject startGameButton;
        
        [Header("Events")]
        [SerializeField] private UnityEvent startGame;
        [SerializeField] private UnityEvent loseGame;
        
        // Private Vars
        private GameObject _activeBiomeObject;
        private Biome _activeBiome;

        private float _currentSpeed;

        private int _segmentsToSpawn;
        
        // Properties
        public float CurrentSpeed => _currentSpeed;
        public bool ImmortalDebugRun => immortalDebugRun;
        public Biome ActiveBiome => _activeBiome;
        public UnityEvent StartGame => startGame;
        public UnityEvent LoseGame => loseGame;
        public GameStates GameState => gameState;
        


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            gameState = GameStates.Idle;
            
            // Add Lose method to the list of methods to be executed by the WorldManagers LoseGame event
            loseGame.AddListener(Lose);
            
            SetUpNewBiome(worldInitPoint);

            TimeManager.Singleton.WorldManager = this;
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
                default:
                    break;
            }
        }


        public void PlayGame()
        {
            gameState = GameStates.Play;
            
            startGameButton.gameObject.SetActive(false);
            
            startGame.Invoke();
        }


        /// <summary>
        /// Randomly Select a biome and Initialize it
        /// </summary>
        public void SetUpNewBiome(Vector3 _biomePlacement)
        {
            // Determine how long this biome will be
            _segmentsToSpawn = Random.Range(minSegmentsPerBiome, maxSegmentsPerBiome + 1);
            
            // Randomly pick a biome and Spawn it
            _activeBiomeObject = Instantiate(
                biomePrefabs[Random.Range(0, biomePrefabs.Count)],
                _biomePlacement,
                quaternion.identity);

            // Reference Biome script
            _activeBiome = _activeBiomeObject.GetComponent<Biome>();

            // Initialize Biome
            _activeBiome.Init(this, _segmentsToSpawn);
        }
        
        
        /// <summary>
        /// Calculate the speed the world should be moving at
        /// </summary>
        private void CalculateCurrentSpeed()
        {
            // If reached max speed, return max speed value
            if (_currentSpeed >= maxSpeed) 
                _currentSpeed = maxSpeed;

            // Current speed is Max Speed * point in the animation curve reached since the run started
            _currentSpeed = maxSpeed * speedMultiplier.Evaluate(TimeManager.Singleton.TimePassed / (minutesTillMaxSpeed * 60));
        }
        
        
        /// <summary>
        /// Gradually but Quickly stop the World Speed
        /// </summary>
        private void SlowToHalt()
        {
            // If reached max speed, return max speed value
            if (_currentSpeed < 0.5f)
            {
                _currentSpeed = 0;
                return;
            }

            // Current speed is Max Speed * point in the animation curve reached since the run started
            _currentSpeed -= (_currentSpeed / 2) * (Time.deltaTime * 4);
        }


        private void Lose()
        {
            if (immortalDebugRun)
                return;
            
            // Set Lose state
            gameState = GameStates.Lose;
            
            // Do score related things
        }
    }
}
