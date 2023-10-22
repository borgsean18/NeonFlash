using System;
using System.Collections.Generic;
using World;
using ProjectTime;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class WorldManager : MonoBehaviour
    {
        // Exposed Vars
        [Header("General Settings")]
        [SerializeField] private Vector3 worldInitPoint;
        
        [Header("Biome Settings")]
        [SerializeField, Min(150)] private int minSegmentsPerBiome;
        [SerializeField, Min(250)] private int maxSegmentsPerBiome;
        [SerializeField] private List<GameObject> biomePrefabs;
        
        [Header("World Movement")]
        [SerializeField] protected float maxSpeed;
        [SerializeField] private AnimationCurve speedMultiplier;
        [SerializeField] private float minutesTillMaxSpeed;
        
        // Private Vars
        private GameObject _activeBiomeObject;
        private Biome _activeBiome;

        private float _currentSpeed;

        private int _segmentsToSpawn;
        
        // Properties
        public float CurrentSpeed => _currentSpeed;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            SetUpNewBiome(worldInitPoint);
        }


        private void Update()
        {
            CalculateCurrentSpeed();
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
    }
}
