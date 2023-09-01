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
    
    public class Segment : MonoBehaviour
    {
        // Private vars
        private SegmentManager _segmentManagerRef;
        
        // Components
        private Rigidbody2D _rb;
        
        public void Init(SegmentManager _segmentManager)
        {
            _segmentManagerRef = _segmentManager;

            _rb = GetComponent<Rigidbody2D>();
            _rb.isKinematic = true;
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
