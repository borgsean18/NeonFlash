using UnityEngine;
using Difficulty;
using MainManagers;

namespace Obstacles
{
	public class Obstacle : MonoBehaviour
	{
		// Components
		private GameManagerScript gameManager;


		private void Awake()
		{
			Init();
		}


		private void Init()
		{
			gameManager = FindObjectOfType<GameManagerScript>();
		}


		private void OnTriggerEnter2D(Collider2D _other)
		{
			// If the obstacle hit the player
			if (_other.gameObject.CompareTag("Player"))
			{
				gameManager.LoseGame.Invoke();
			}
		}
	}
}
