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
        
        // Private Vars
        SpawningManager spawningManager;
        DifficultyObject difficultyObject;
        
        // Components
        public DifficultyObject DifficultyObject => difficultyObject;


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
    }
}
