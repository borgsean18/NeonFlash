using UnityEngine;
using World;

namespace CleanUp
{
	public class SegmentCleanUp : CleanUp
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
		
		
		protected override void OnTriggerExit2D(Collider2D _other)
		{
			// If the segment has exited the clean up object bounds
			if (!_other.gameObject.CompareTag("CleanUp"))
				return;
			
			if (_segment != null && _segment.ParentBiome != null)
				_segment.ParentBiome.UnLoadSegment(_segment);
			
			Destroy(gameObject);
		}
	}
}
