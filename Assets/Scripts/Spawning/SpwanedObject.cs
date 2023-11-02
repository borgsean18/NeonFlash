using System;
using Difficulty;
using UnityEngine;

namespace Spawning
{
    [RequireComponent(typeof(DifficultyObject))]
    public class SpwanedObject : MonoBehaviour
    {
        // Private Vars
        DifficultyObject difficultyObject;
        
        // Components
        public DifficultyObject DifficultyObject => difficultyObject;
        

        void Awake()
        {
            Init();
        }
        

        private void Init()
        {
            difficultyObject = GetComponent<DifficultyObject>();
        }
    }
}
