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


        private void Update()
        {
            if (!canTravel)
                return;
            
            TravelToTarget();
        }


        private void TravelToTarget()
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject == projectile.Owner)
                return;

            TakeDamage td = other.gameObject.GetComponent<TakeDamage>();

            if (td != null)
            {
                td.RecieveDamage(projectile.Damage);

                Destroy(gameObject);
            }
        }
    }
}
