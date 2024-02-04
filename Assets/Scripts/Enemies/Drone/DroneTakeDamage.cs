using Combat;
using UnityEngine;

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


        protected override void Awake()
        {
            anim = GetComponent<Animator>();

            rb = GetComponent<Rigidbody2D>();
        }


        void Update()
        {

#if UNITY_EDITOR

            DebugDeath();

#endif

        }


        protected override void Die()
        {
            // Play Death animation
            anim.SetBool("Death", true);

            // Kill the drone
            droneBehaviour.Die();

            // Enable Rigidbody gravity
            rb.gravityScale = 1.0f;
            rb.isKinematic = false;
        }


        #region Debug Methods

        private void DebugDeath()
        {
            if (die)
            {
                die = false;
                Die();
            }
        }

        #endregion
    }
}
