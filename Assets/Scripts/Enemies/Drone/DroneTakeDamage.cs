using System;
using System.Collections;
using Combat;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Enemies.Drone
{
    public class DroneTakeDamage : TakeDamage
    {
        // Exposed Variables
        [Header("Drone Specifics")]
        [SerializeField] private DroneBehaviour droneBehaviour;

        [Header("Debugging")]
        [SerializeField] private bool die;
        
        
        // Components
        private Animator anim;
        private Rigidbody2D rb;


        void Awake()
        {
            anim = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();
        }


        void Update()
        {
            if (die)
            {
                die = false;
                Die();
            }
        }


        protected override void Die()
        {
            // Play Death animation
            anim.SetBool("Death", true);

            // Kill the drone
            droneBehaviour.Die();

            StartCoroutine(DisableAnimatorAfterDeath());
        }


        IEnumerator DisableAnimatorAfterDeath()
        {
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            // yield return new WaitForSeconds(0);

            // Disable animator
            anim.enabled = false;

            // Enable Rigidbody gravity
            rb.isKinematic = false;
            rb.gravityScale = 1.0f;
        }
    }
}
