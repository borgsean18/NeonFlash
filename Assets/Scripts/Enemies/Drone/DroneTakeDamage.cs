using System;
using Combat;
using UnityEngine;

namespace Enemies.Drone
{
    public class DroneTakeDamage : TakeDamage
    {
        // Exposed Variables
        [Header("Drone Specifics")]
        [SerializeField] private DroneBehaviour droneBehaviour;
        
        
        // Components
        private Animator anim;
        private Rigidbody2D rb;


        void Awake()
        {
            anim = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();
        }


        protected override void Die()
        {
            droneBehaviour.Die();

            // Fall to the ground
            rb.gravityScale = 1;
            
            // Stop all animations
            anim.enabled = false;
        }
    }
}
