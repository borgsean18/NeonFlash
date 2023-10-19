using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Obstacles
{
	public class Obstacle : MonoBehaviour
	{
		// Components
		private BoxCollider2D boxCollider2D;
		private SpriteRenderer spriteRenderer;


		private void Awake()
		{
			
		}


		private void Init()
		{
			boxCollider2D.size = spriteRenderer.bounds.size;
		}


		private void OnCollisionEnter2D(Collision2D _col)
		{
			if(_col.gameObject.CompareTag("Player"))
				Destroy(_col.gameObject);
		}
	}
}
