using System;
using System.Collections;
using System.Collections.Generic;
using Difficulty;
using MainManagers;
using Unity.VisualScripting;
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

        [Header("Testing Settings")]
        [SerializeField] bool spawnTestObject;
        [SerializeField] GameObject testObject;

        
        // Private Variables
        private GameManagerScript gameManager;
        private WorldManager worldManager;
        private DifficultyManager difficultyManager;
        private List<SpwanedObject> spawnedObjects;
        private List<string> coolDownGUIDs; 
        private bool spawnCooldownActive;


        private void Start()
        {
            Init();
        }


        private void Init()
        {
            gameManager = GetComponent<GameManagerScript>();
            worldManager = GetComponent<WorldManager>();
            difficultyManager = GetComponent<DifficultyManager>();

            spawnedObjects = new List<SpwanedObject>();
            coolDownGUIDs = new List<string>();
            
            StartCoroutine(SpawningCoolDown(initSpawnCooldown));
        }


        private void Update()
        {
            // If the spawn cooldown is over and the game is still playing
            if (!spawnCooldownActive && gameManager.GameState == GameStates.Play)
                Spawn(CanSpawn());

            if (spawnTestObject && testObject != null)
                DebugSpawn(testObject);
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

            int spawnAllowance = (int)difficultyManager.CurrentDifficulty - totalSpawnedDifficulty;

            if (spawnAllowance < 0)
                spawnAllowance = 0;
            
            return spawnAllowance;
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
            
            InitialiseSpawnedObject(spawnedObject);

            DistanceSpawns();
            
            StartCoroutine(SpawningCoolDown(timeBetweenSpawns));
        }


        private void DebugSpawn(GameObject _gameObject)
        {
            spawnTestObject = false;
            
            GameObject spawnedObject = Instantiate(_gameObject, obstacleSpawnPoint.position, Quaternion.identity);

            InitialiseSpawnedObject(spawnedObject);
        }


        private void InitialiseSpawnedObject(GameObject _spawnedObj)
        {
            SpwanedObject spawnedObjComponent = _spawnedObj.GetComponent<SpwanedObject>();
            
            spawnedObjComponent.Init(this);
            
            spawnedObjects.Add(spawnedObjComponent);
        }


        private GameObject SelectWhatToSpawn(GameObject _spawnablesHaver, int _spawnAllowance)
        {
            SpawnsList list = _spawnablesHaver.GetComponent<SpawnsList>();

            GameObject toSpawn;
            
            toSpawn = list.GetRandomObstacle(_spawnAllowance, coolDownGUIDs);
            
            return toSpawn;
        }


        private IEnumerator SpawningCoolDown(float _coolDown)
        {
            spawnCooldownActive = true;
            
            yield return new WaitForSeconds(UnityEngine.Random.Range(_coolDown / 2f, _coolDown));
            
            spawnCooldownActive = false;
        }


        /// <summary>
        /// Space out spawned objects to never have any spawns be too close to each other
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


        /// <summary>
        /// Using GUIDs limit how often an enemy can be spawned in a row. GUIDs have to be used
        /// because stopping an enemy being spawned in a biome doesnt stop that enemy being spawned
        /// in a different biome
        /// </summary>
        public void CoolDownSpecificSpawn(string spawnGUID, float coolDown)
        {
            StartCoroutine(CoolDownSpecificSpawnCoRt(spawnGUID, coolDown));
        }


        private IEnumerator CoolDownSpecificSpawnCoRt(string spawnGUID, float coolDown)
        {
            if (!coolDownGUIDs.Contains(spawnGUID))
                coolDownGUIDs.Add(spawnGUID);
            else
                yield break;

            yield return new WaitForSeconds(coolDown);

            if (coolDownGUIDs.Contains(spawnGUID))
                coolDownGUIDs.Remove(spawnGUID);
        }
    }
}