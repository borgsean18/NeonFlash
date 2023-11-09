using System;
using Obstacles;
using UnityEngine;

namespace Combat
{
    public class MeleWeapon : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D _other)
        {
            DestructableObstacle destructableObstacle = _other.GetComponent<DestructableObstacle>();

            if (destructableObstacle == null) return;
            
            destructableObstacle.DestroyObject();
        }
    }
}
