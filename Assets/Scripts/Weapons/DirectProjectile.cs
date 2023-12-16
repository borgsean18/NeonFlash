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
        GameObject _firer;
        
        
        public void Init(Transform target, GameObject firer)
        {
            _target = target;
            _targetPos = _target.position;
            _firer = firer;
            
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
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == _firer)
                return;

            float destroyProjectileTime = 0;
            
            // If collided with another layer, travel perpendicular to collision
            if (other.gameObject.layer == 7)
            {
                speed += 2f;
                
                // Get the direction of the trigger collision
                Vector3 triggerDirection = other.transform.position - transform.position;

                // Calculate the angle in radians from the trigger direction
                float angle = Mathf.Atan2(triggerDirection.y, triggerDirection.x);
                angle *= Mathf.Rad2Deg; // Convert radians to degrees

                // Set the rotation to face the trigger direction
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
                
                destroyProjectileTime += 6f;
            }

            // Start timer to destroy this projectile
            StartCoroutine(DestroyProjectile(destroyProjectileTime));
        }


        private IEnumerator DestroyProjectile(float _time)
        {
            yield return new WaitForSeconds(_time);

            Destroy(gameObject);
        }
    }
}
