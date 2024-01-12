using System;
using Difficulty;
using UnityEngine;

namespace Spawning
{
    [RequireComponent(typeof(DifficultyObject))]
    public class SpwanedObject : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private Vector3 startPosition;

        [Header("CoolDown")]
        [SerializeField] private string spawnableGUID;
        [SerializeField, Min(0)] private float coolDownLength = 0;
        
        
        // Private Vars
        SpawningManager spawningManager;
        DifficultyObject difficultyObject;
        
        
        // Components
        public DifficultyObject DifficultyObject => difficultyObject;
        public string SpawnableGUID => spawnableGUID;


        public void Init(SpawningManager _spawningManager)
        {
            spawningManager = _spawningManager;
            difficultyObject = GetComponent<DifficultyObject>();

            PositionObstacle();
        }


        private void PositionObstacle()
        {
            transform.position = startPosition;
        }


        private void OnDestroy()
        {
            SetUpCoolDown();
        }


        private void SetUpCoolDown()
        {
            // Do nothing if these fields are not filled
            if (coolDownLength == 0 || spawnableGUID == "") return;

            spawningManager.CoolDownSpecificSpawn(spawnableGUID, coolDownLength);
        }
    }
}
