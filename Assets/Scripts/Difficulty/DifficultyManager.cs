using UnityEngine;
using ProjectTime;

namespace Difficulty
{
	public class DifficultyManager : MonoBehaviour
	{
		// Exposed Variables
		[SerializeField] private int maxDifficulty;
		[SerializeField] private float minutesTillMaxDifficulty;
		[SerializeField] private AnimationCurve difficultyCurveOverTime;
		
		// Private Variables
		private float _currentDifficulty;
		private DifficultyObject _difficultyDampener;
		
		// Properties
		public int MaxDifficulty => maxDifficulty;
		public float CurrentDifficulty => _currentDifficulty;


		private void Update()
		{
			CalculateDifficulty();
		}


		/// <summary>
		/// Calculate the difficulty level the game has reached at given time
		/// </summary>
		private void CalculateDifficulty()
		{
			// If reached max speed, return max speed value
			if (_currentDifficulty >= maxDifficulty) 
				_currentDifficulty = maxDifficulty;

			// Current speed is Max Speed * point in the animation curve reached since the run started
			_currentDifficulty = maxDifficulty * difficultyCurveOverTime.Evaluate(TimeManager.Singleton.TimePassed / (minutesTillMaxDifficulty * 60));

			// If the difficulty should be dampened, set it to the dampened level
			if (_difficultyDampener != null)
			{
				_currentDifficulty /= 2;

				if (_currentDifficulty > _difficultyDampener.MaxDifficultySetting)
					_currentDifficulty = _difficultyDampener.MaxDifficultySetting;
			}
		}


		/// <summary>
		/// Used when spawning difficult enemies, to not spawn too many obstacles while a boss is active
		/// </summary>
		public void DampenDifficulty(DifficultyObject difficultyDampener)
		{
			_difficultyDampener = difficultyDampener;
		}


		public void ReturnDifficultyToNormal()
		{
			_difficultyDampener = null;
		}
	}
}
