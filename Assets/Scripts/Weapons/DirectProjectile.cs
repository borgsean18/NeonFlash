using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Weapons
{
    public class DirectProjectile : MonoBehaviour
    {
        // Private Variables
        private bool canTravel;
        
        public void LookAtTarget(Transform _target)
        {
            // Look at target
            transform.right = _target.position - transform.position;

            Travel();
        }


        private void Travel()
        {
            canTravel = true;
        }


        void Update()
        {
            if (!canTravel)
                return;
            
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
            
            // If collided with an object that can redirect projectiles
            if (other.gameObject.GetComponent<RedirectProjectile>() != null)
            {
                other.gameObject.GetComponent<RedirectProjectile>().RedirectProjectile(transform);
                
                destroyProjectileTime += 5f;
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
