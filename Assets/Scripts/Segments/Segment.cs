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
        private Biome _parentBiomeRef;
        
        // Components
        private Rigidbody2D _rb;
        private SpriteRenderer _spriteRenderer;
        
        public void Init(Biome _parentBiome)
        {
            _parentBiomeRef = _parentBiome;
            
            _rb = GetComponent<Rigidbody2D>();
            _rb.isKinematic = true;

            _spriteRenderer = GetComponent<SpriteRenderer>();
        }


        public float SegmentWidth()
        {
            return _spriteRenderer.bounds.size.x;
        }


        /// <summary>
        /// Player Left the segment
        /// </summary>
        private void OnTriggerExit2D(Collider2D _other)
        {
            if (!_other.gameObject.CompareTag("Player")) return;
            
            // Trigger Delete Method
        }
    }
}
