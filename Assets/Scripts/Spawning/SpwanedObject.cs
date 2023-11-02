using System;
using Difficulty;
using UnityEngine;

namespace Spawning
{
    [RequireComponent(typeof(DifficultyObject))]
    public class SpwanedObject : MonoBehaviour
    {
        // Private Vars
        SpawningManager spawningManager;
        DifficultyObject difficultyObject;
        
        // Components
        public DifficultyObject DifficultyObject => difficultyObject;


        public void Init(SpawningManager _spawningManager)
        {
            spawningManager = _spawningManager;
            difficultyObject = GetComponent<DifficultyObject>();
        }
    }
}
