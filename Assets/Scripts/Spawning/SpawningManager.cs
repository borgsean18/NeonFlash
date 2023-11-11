using System.Collections;
using System.Collections.Generic;
using Difficulty;
using MainManagers;
using UnityEngine;
using World;

namespace Spawning
{
    [RequireComponent(typeof(WorldManager), typeof(DifficultyManager))]
    public class SpawningManager : MonoBehaviour
    {
        // Exposed Variables
        [Header("Timers")]
        [SerializeField] private float initSpawnCooldown;
        [SerializeField] private float timeBetweenSpawns;
        
        [Header("Positioning")]
        [SerializeField] private Transform obstacleSpawnPoint;
        [SerializeField] private float minDistanceBetweenSpawns;

        
        // Private Variables
        private GameManagerScript gameManager;
        private WorldManager worldManager;
        private DifficultyManager difficultyManager;
        private List<SpwanedObject> spawnedObjects;
        private bool canSpawn;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            gameManager = GetComponent<GameManagerScript>();
            worldManager = GetComponent<WorldManager>();
            difficultyManager = GetComponent<DifficultyManager>();

            spawnedObjects = new List<SpwanedObject>();
            
            StartCoroutine(SpawnCoolDown(initSpawnCooldown));
        }


        private void Update()
        {
            // If the spawn cooldown is over and the game is still playing
            if (canSpawn && gameManager.GameState == GameStates.Play)
                Spawn(CanSpawn());
        }
        
        
        // Determine when to spawn something based on the current difficulty
        private int CanSpawn()
        {
            int totalSpawnedDifficulty = 0;
            
            // Remove the Deleted objects from the list
            spawnedObjects.RemoveAll(item => item == null);
            
            foreach (var obj in spawnedObjects)
            {
                totalSpawnedDifficulty += obj.DifficultyObject.DifficultyLevel;
            }
            
            return (int)difficultyManager.CurrentDifficulty - totalSpawnedDifficulty;
        }


        // Spawn an enemy or obstacle depending on the difficulty level
        private void Spawn(int _spawnAllowance)
        {
            // Cant spawn anything
            if (_spawnAllowance == 0)
                return;
            
            // Pick what to spawn based on allowance
            GameObject toSpawn = SelectWhatToSpawn(worldManager.ActiveBiome.gameObject, _spawnAllowance);

            // SPAWN THE OBJECT
            GameObject spawnedObject = Instantiate(toSpawn, obstacleSpawnPoint.position, Quaternion.identity);
            
            SpwanedObject spawnedObjComponent = spawnedObject.GetComponent<SpwanedObject>();

            spawnedObjComponent.Init(this);

            spawnedObjects.Add(spawnedObjComponent);

            DistanceSpawns();
            
            StartCoroutine(SpawnCoolDown(timeBetweenSpawns));
        }


        private GameObject SelectWhatToSpawn(GameObject _spawnablesHaver, int _spawnAllowance)
        {
            SpawnsList list = _spawnablesHaver.GetComponent<SpawnsList>();
            
            GameObject toSpawn = list.GetRandomObstacle();

            DifficultyObject difficultyObject = toSpawn.GetComponent<DifficultyObject>();

            while (difficultyObject.DifficultyLevel > _spawnAllowance || difficultyObject == null)
            {
                toSpawn = list.GetRandomObstacle();
                difficultyObject = toSpawn.GetComponent<DifficultyObject>();
            }
            
            return toSpawn;
        }


        private IEnumerator SpawnCoolDown(float _coolDown)
        {
            canSpawn = false;
            
            yield return new WaitForSeconds(Random.Range(_coolDown / 2.5f, _coolDown));
            
            canSpawn = true;
        }


        /// <summary>
        /// Purpose of this method is to space out spawned objects to never have any spawns
        /// be too close to each other
        /// </summary>
        private void DistanceSpawns()
        {
            // Do nothing if theres just 1 or less spawns
            if (spawnedObjects.Count <= 1) return;

            // Get the last 2 spawned objects
            var lastSpawned = spawnedObjects[^1];
            var secondLastSpawned = spawnedObjects[^2];

            // Get a reference to their transforms
            Transform lastSpawnedTransform = lastSpawned.transform;
            Transform secondLastSpawnedTransform = secondLastSpawned.transform;

            // Check the distance between them
            float distance = Vector2.Distance(lastSpawnedTransform.position, 
                secondLastSpawnedTransform.position);

            // Do nothing if the last 2 spawns are already spaced enough
            if (distance >= minDistanceBetweenSpawns)
                return;

            // Space them out to the minimum allowed spacing
            Vector2 newPos = lastSpawnedTransform.position;
            newPos.x = secondLastSpawnedTransform.position.x + minDistanceBetweenSpawns;
            lastSpawnedTransform.position = newPos;
        }
    }
}