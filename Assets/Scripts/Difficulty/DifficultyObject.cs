using UnityEngine;

namespace Difficulty
{
	public class DifficultyObject : MonoBehaviour
	{
        // Exposed Variables
        [Header("Difficulty Settings")]
        [SerializeField] private int difficultyLevel;

        [Header("Difficulty Dampening")]
        [SerializeField] private bool dampenDifficulty;
        [SerializeField] private int newMaxDifficulty;


        // Private Variables
        private DifficultyManager _difficultyManager;


        // Properties
        public int DifficultyLevel => difficultyLevel;
        public int MaxDifficultySetting => newMaxDifficulty;


        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            if (dampenDifficulty)
            {
                _difficultyManager = FindObjectOfType<DifficultyManager>();
                _difficultyManager.DampenDifficulty(this);
            }
        }


        private void OnDestroy()
        {
            if (dampenDifficulty && _difficultyManager != null)
                _difficultyManager.ReturnDifficultyToNormal();
        }
    }
}
