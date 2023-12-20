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
        private float speed;
        private bool canTravel;
        private GameObject owner;
        

        
        public void SetUpMovement(Transform _target, float _speed, GameObject _owner)
        {
            speed = _speed;
            owner = _owner;
            
            // Look at target
            transform.right = _target.position - transform.position;

            canTravel = true;
        }


        public void UpdateOwner(GameObject _newOwner)
        {
            owner = _newOwner;
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
            if (other.gameObject == owner)
                return;

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
