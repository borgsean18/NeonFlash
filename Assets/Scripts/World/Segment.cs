using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEngine;
using World;

namespace World
{
	public class Segment : MonoBehaviour
	{
		// Private Vars
		private Biome _parentBiome;
		
		// Components
		private BoxCollider2D _boxCollider2D;
		
		// Properties
		public Biome ParentBiome => _parentBiome;


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
	}
}