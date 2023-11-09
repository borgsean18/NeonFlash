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
		}
	}
}
