using System;
using UnityEngine;
using World;

namespace CleanUp
{
	public class SegmentCleanUo : CleanUp
	{
		private Segment _segment;


		private void Awake()
		{
			Init();
		}


		private void Init()
		{
			_segment = GetComponent<Segment>();
		}
		
		
		/// <summary>
		/// When the player exits this segment
		/// </summary>
		protected override void OnTriggerExit2D(Collider2D _other)
		{
			// If the segment has exited the clean up object bounds
			if (!_other.gameObject.CompareTag("CleanUp"))
				return;
			
			_segment.ParentBiome.UnLoadSegment(_segment);
			
			Destroy(gameObject);
		}
	}
}
