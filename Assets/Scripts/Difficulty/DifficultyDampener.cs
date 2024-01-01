using System;
using UnityEngine;

namespace Difficulty
{
    public class DifficultyDampener : MonoBehaviour
    {
        // Private Variables
        private DifficultyManager _difficultyManager;

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
