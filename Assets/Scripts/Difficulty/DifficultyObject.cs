using UnityEngine;

namespace Difficulty
{
	public class DifficultyObject : MonoBehaviour
	{
		// Exposed Variables
		[SerializeField, Min(1)] private int difficultyLevel;
		
		// Properties
		public int DifficultyLevel => difficultyLevel;
	}
}
