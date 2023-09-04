using System;
using UnityEngine;

namespace Segments
{
	public class Segment : MonoBehaviour
	{
		// Private Vars
		private Biome _parentBiome;
		
		// Components
		private BoxCollider2D _boxCollider2D;
		

		public void Init(Biome _parentBiome)
		{
			this._parentBiome = _parentBiome;
			
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}
		
		
		public float SegmentWidth()
		{
			return _boxCollider2D.bounds.size.x;
		}

		
		/// <summary>
		/// When the player exits this segment
		/// </summary>
		private void OnCollisionExit2D(Collision2D _other)
		{
			// Do nothing if not the player
			if (!_other.gameObject.CompareTag("Player"))
				return;
			
			
		}
	}
}