using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Weapons
{
    public class RedirectProjectile : MonoBehaviour
    {
        public void RedirectProjectile(GameObject _projectile)
        {
            speed += 2f;

            float randomRotation = Random.Range(160f, 180f);
            
            _projectile.transform.rotation = Quaternion.Euler(0f, 0f, randomRotation);
        }
    }
}
