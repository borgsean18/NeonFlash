using System;
using System.Collections;
using System.Collections.Generic;
using Difficulty;
using Unity.VisualScripting;
using UnityEngine;

namespace Spawning
{
    [RequireComponent(typeof(DifficultyManager))]
    public class SpawningManager : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private float timeBetweenSpawns;
        
        // Private Variables
        private DifficultyManager difficultyManager;
        private List<SpwanedObject> spawnedObjects;
        private bool spawnCoolDownActive;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            difficultyManager = GetComponent<DifficultyManager>();

            spawnedObjects = new List<SpwanedObject>();
        }


        private void Update()
        {
            if (CanSpawn())
                Spawn();
        }
        
        
        // Determine when to spawn something based on the current difficulty
        private bool CanSpawn()
        {
            if (spawnedObjects.Count <= 1 && !spawnCoolDownActive)
                return true;
            
            return false;
        }


        // Spawn an enemy or obstacle depending on the difficulty level
        private void Spawn()
        {
            StartCoroutine(SpawnCoolDown());
        }


        private IEnumerator SpawnCoolDown()
        {
            spawnCoolDownActive = true;
            
            yield return new WaitForSeconds(0.3f);
            
            spawnCoolDownActive = false;
        }
    }
}