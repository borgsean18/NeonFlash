using System;
using UnityEngine;

namespace Segments
{
	public class Segment : MonoBehaviour
	{
		// Private Vars
		private Biome _parentBiome;
		private bool _isBiomeTrigger;
		
		// Components
		private BoxCollider2D _boxCollider2D;
		
		// Properties
		public bool IsBiomeTrigger => _isBiomeTrigger;
		

		public void Init(Biome _parentBiome, bool _isBiomeTrigger = false)
		{
			this._parentBiome = _parentBiome;
			this._isBiomeTrigger = _isBiomeTrigger;
			
			_boxCollider2D = GetComponent<BoxCollider2D>();
		}
		
		
		public float SegmentWidth()
		{
			return _boxCollider2D.bounds.size.x;
		}

		
		/// <summary>
		/// When the player exits this segment
		/// </summary>
		private void OnTriggerExit2D(Collider2D _other)
		{
			// If the segment has exited the clean up object bounds
			if (!_other.gameObject.CompareTag("CleanUp"))
				return;
			
			_parentBiome.DeleteSegments(this);
		}
	}
}