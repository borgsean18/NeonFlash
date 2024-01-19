using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;

namespace Weapons
{
    public class RedirectProjectile : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private GameObject projectileNewOwner;
        
        
        // Private Variables
        private bool _canRedirect = true;
        
        
        public void Redirect(Transform _projectile)
        {
            if (!_canRedirect) return;

            // Safety check so projectiles can only be redirected max of 1 time
            _canRedirect = true;
            
            _projectile.transform.Rotate(Vector3.forward, 180, Space.World);

            Projectile proj = _projectile.gameObject.GetComponent<Projectile>();

            if (proj != null && projectileNewOwner != null)
                proj.ChangeOwner(projectileNewOwner);
        }


        private void OnTriggerEnter2D(Collider2D _collision)
        {
            if (_collision.gameObject.GetComponent<DirectProjectile>() == null)
                return;

            Redirect(_collision.transform);
        }
    }
}
