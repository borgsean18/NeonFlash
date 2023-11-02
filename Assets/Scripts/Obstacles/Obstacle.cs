using UnityEngine;
using Difficulty;
using MainManagers;

namespace Obstacles
{
	[RequireComponent(typeof(DifficultyObject))]
	public class Obstacle : MonoBehaviour
	{
		// Components
		private WorldManager worldManager;
		private DifficultyObject difficultySettings;
		private SpriteRenderer spriteRenderer;
		private BoxCollider2D boxCollider2D;


		private void Awake()
		{
			Init();
		}


		private void Init()
		{
			worldManager = FindObjectOfType<WorldManager>();
			
			difficultySettings = GetComponent<DifficultyObject>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			
			boxCollider2D = GetComponent<BoxCollider2D>();
			boxCollider2D.size = spriteRenderer.bounds.size;
		}


		private void OnTriggerEnter2D(Collider2D _other)
		{
			// If the obstacle hit the player
			if (_other.gameObject.CompareTag("Player"))
			{
				worldManager.LoseGame.Invoke();
			}
		}
	}
}
