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
        [SerializeField] private float initSpawnCooldown;
        [SerializeField] private float timeBetweenSpawns;
        [SerializeField] private Transform obstacleSpawnPoint;

        
        // Private Variables
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
            worldManager = GetComponent<WorldManager>();
            difficultyManager = GetComponent<DifficultyManager>();

            spawnedObjects = new List<SpwanedObject>();
            
            StartCoroutine(SpawnCoolDown(initSpawnCooldown));
        }


        private void Update()
        {
            if (canSpawn)
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
            Biome activeBiome = worldManager.ActiveBiome;
            
            GameObject toSpawn = SelectWhatToSpawn(activeBiome.gameObject, _spawnAllowance);

            GameObject spawnedObject = Instantiate(toSpawn, obstacleSpawnPoint.position, Quaternion.identity);
            
            SpwanedObject spawnedObjComponent = spawnedObject.GetComponent<SpwanedObject>();

            spawnedObjComponent.Init(this);

            spawnedObjects.Add(spawnedObjComponent);
            
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
            
            yield return new WaitForSeconds(_coolDown);
            
             canSpawn = true;
        }
    }
}