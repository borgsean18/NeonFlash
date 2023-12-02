using UnityEngine;

namespace Difficulty
{
	public class DifficultyObject : MonoBehaviour
	{
		// Exposed Variables
		[SerializeField] private int difficultyLevel;
		
		// Properties
		public int DifficultyLevel => difficultyLevel;
	}
}
