using System;
using UnityEngine;

namespace Difficulty
{
    public class DifficultyDampener : MonoBehaviour
    {
        // Exposed Variables
        [Header("Update Difficulty Settings")]
        [SerializeField] private int _newMaxDifficulty;
        
        
        // Private Variables
        private DifficultyManager _difficultyManager;
        
        
        // Properties
        public int MaxDifficultySetting => _newMaxDifficulty;

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            _difficultyManager = FindObjectOfType<DifficultyManager>();
            _difficultyManager.DampenDifficulty(this);
        }


        private void OnDestroy()
        {
            _difficultyManager.ReturnDifficultyToNormal();
        }
    }
}
