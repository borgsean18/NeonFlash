using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Segments
{
	public class Segment : MonoBehaviour
	{
		// Private Vars
		private Biome _parentBiome;
		
		// Components
		private BoxCollider2D _boxCollider2D;
		

		/// <summary>
		/// Initialize this segment
		/// </summary>
		/// <param name="_parentBiome">Keep a reference to the parent biome</param>
		public void Init(Biome _parentBiome)
		{
			this._parentBiome = _parentBiome;
			
			_boxCollider2D = GetComponent<BoxCollider2D>();

			// Check if there are other segments in biome to determine this Segment XPos
			if (this._parentBiome.ActiveSegments.Count == 0)
				PositionSegment(null);
			else
				PositionSegment(this._parentBiome.ActiveSegments[^1]);
		}
		
		
		public float SegmentWidth()
		{
			return _boxCollider2D.bounds.size.x;
		}


		/// <summary>
		/// Position the segment depending on where the last segment was, if there
		/// were any previous segments at all
		/// </summary>
		/// <param name="_prevSegment">The last segment spawned in this biome (if any exists)</param>
		private void PositionSegment([CanBeNull] Segment _prevSegment)
		{
			float newX;
			
			// If this is the first segment in the biome
			if (_prevSegment == null)
			{
				newX = _parentBiome.transform.position.x;

				newX += SegmentWidth() / 2;
				
				// Spawn the segment at the Biomes XPos, adjusted for the segments width
				transform.position = new Vector3(newX, 0);
				
				return;
			}

			// Get the XPos of the previous segment
			float prevX = _prevSegment.transform.position.x;
			
			// Get the Width of the previous segment
			float prevWidth = _prevSegment.SegmentWidth();

			// New XPos is the previous segment XPos + (its width / 2)
			newX = prevX + (prevWidth / 2);

			// Add the Width / 2 of this segment to that XPos value
			newX += SegmentWidth() / 2;

			// Move this Segment to the newly calculated position (touching the previous segment)
			transform.position = new Vector3(newX, 0);
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