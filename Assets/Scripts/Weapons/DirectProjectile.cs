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
