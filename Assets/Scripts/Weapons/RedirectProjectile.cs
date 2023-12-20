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
        
        
        public void Redirect(Transform _projectile)
        {
            Quaternion rotation = _projectile.transform.rotation;
            rotation.z *= -2;
            
            _projectile.transform.rotation = rotation;

            Projectile proj = _projectile.gameObject.GetComponent<Projectile>();

            if (proj != null && projectileNewOwner != null)
                proj.ChangeOwner(projectileNewOwner);
        }
    }
}
