using UnityEngine;
using ProjectTime;
using System.Collections.Generic;

namespace Difficulty
{
	public class DifficultyManager : MonoBehaviour
	{
		// Exposed Variables
		[SerializeField] private int maxDifficulty;
		[SerializeField] private float minutesTillMaxDifficulty;
		[SerializeField] private AnimationCurve difficultyCurveOverTime;

		
		// Private Variables
		private bool _init;
		private float _difficultyTimer;
		private float _currentDifficulty;
		private int _originalMaxDifficulty;
		private bool _isPaused;
		private List<DifficultyObject> _difficultyDampeners;

		
		// Properties
		public int MaxDifficulty => maxDifficulty;
		public float CurrentDifficulty => _currentDifficulty;


		private void Awake()
		{
			Init();
		}


		private void Init()
		{
			_isPaused = true;
			_difficultyTimer = 0;
			_originalMaxDifficulty = maxDifficulty;
			_difficultyDampeners = new List<DifficultyObject>();
			
			_init = true;
		}


		private void Update()
		{
			if (!_init || _isPaused)
				return;
            
			_difficultyTimer += Time.deltaTime;

			CalculateDifficulty();
		}


		/// <summary>
		/// Calculate the difficulty level the game has reached at given time
		/// </summary>
		private void CalculateDifficulty()
		{
			if (_difficultyDampeners.Count > 0)
				maxDifficulty = _difficultyDampeners[^1].NewMaxDifficulty; // Max Diff of the last added enemy
			else
				maxDifficulty = _originalMaxDifficulty;

			// If reached max difficulty, return max difficulty value
			if (_currentDifficulty >= maxDifficulty) 
			{
				_currentDifficulty = maxDifficulty;
				return;
			}

			// Current difficulty is Max Difficulty * point in the animation curve reached since the run started
			_currentDifficulty = maxDifficulty * difficultyCurveOverTime.Evaluate(_difficultyTimer / (minutesTillMaxDifficulty * 60));			
		}


		/// <summary>
		/// Used when spawning difficult enemies, to not spawn too many obstacles while a boss is active
		/// </summary>
		public void DampenDifficulty(DifficultyObject difficultyDampener)
		{
			_difficultyDampeners.Add(difficultyDampener);
		}


		public void RemoveDampener(DifficultyObject difficultyDampener)
		{
			_difficultyDampeners.Remove(difficultyDampener);
		}


		/// <summary>
		/// Called in the GameManager Start UnityEvent.
		/// Called in any Resume Button Events (None yet).
		/// </summary>
		public void ResumeDifficulty()
		{
			_isPaused = false;
		}


		public void PauseDifficulty()
		{
			_isPaused = true;
		}
	}
}
