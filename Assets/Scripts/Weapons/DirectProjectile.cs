using System;
using System.Collections;
using Combat;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Weapons
{
    [RequireComponent(typeof(Projectile))]
    public class DirectProjectile : MonoBehaviour
    {
        // Private Variables
        private float speed;
        private bool canTravel;
        private Projectile projectile;
        

        
        public void SetUpMovement(Transform _target, Projectile _projectile)
        {
            projectile = _projectile;
            speed = projectile.Speed;
            
            // Look at target
            transform.right = _target.position - transform.position;

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
            if (other.gameObject == projectile.Owner)
                return;

            TakeDamage td = other.gameObject.GetComponent<TakeDamage>();
            
            if (td != null)
            {
                td.TakeDamageMethod(projectile.Damage);
            }

            // How long the delay would be for this projectile to be deleted
            float destroyProjectileTime = 0;
            
            // If collided with an object that can redirect projectiles
            if (other.gameObject.GetComponent<RedirectProjectile>() != null)
            {
                other.gameObject.GetComponent<RedirectProjectile>().Redirect(transform);
                
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
