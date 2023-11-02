using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Difficulty;

namespace Obstacles
{
	[RequireComponent(typeof(DifficultyObject))]
	public class Obstacle : MonoBehaviour
	{
		// Components
		private DifficultyObject difficultySettings;
		private SpriteRenderer spriteRenderer;
		private BoxCollider2D boxCollider2D;


		private void Awake()
		{
			Init();
		}


		private void Init()
		{
			difficultySettings = GetComponent<DifficultyObject>();
			spriteRenderer = GetComponent<SpriteRenderer>();
			
			boxCollider2D = GetComponent<BoxCollider2D>();
			boxCollider2D.size = spriteRenderer.bounds.size;

		}


		// private void OnCollisionEnter2D(Collision2D _col)
		// {
		// 	if(_col.gameObject.CompareTag("Player"))
		// 		Destroy(_col.gameObject);
		// }
	}
}
