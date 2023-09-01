using System;
using Managers;
using UnityEngine;

namespace Segments
{
    [RequireComponent(
        typeof(SpriteRenderer), 
        typeof(Rigidbody2D),
        typeof(BoxCollider2D)
        )]
    
    public class Segment : Biome
    {
        // Private vars
        private SegmentManager _segmentManagerRef;
        
        
        public void Init(SegmentManager _segmentManager)
        {
            _segmentManagerRef = _segmentManager;
        }


        /// <summary>
        /// Player Left the segment
        /// </summary>
        private void OnTriggerExit2D(Collider2D _other)
        {
            if (!_other.gameObject.CompareTag("Player")) return;
            
            _segmentManagerRef.DeleteUsedSegments(this);
        }
    }
}
