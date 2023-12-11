using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Weapons
{
    public class DirectProjectile : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] private float speed;
        
        
        // Private Variables
        Transform _target;
        Vector3 _targetPos;
        
        
        public void Init(Transform target)
        {
            _target = target;
            _targetPos = _target.position;
            
            // Look at target
            transform.right = _targetPos - transform.position;
        }


        void Update()
        {
            if (_target == null) Destroy(gameObject);
            
            TravelToTarget();
        }


        private void TravelToTarget()
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                _targetPos, 
                speed * Time.deltaTime
                );
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                Destroy(gameObject);
        }
    }
}
