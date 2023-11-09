using System;
using UnityEngine;

namespace Combat
{
    public class CombatScript : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] protected float coolDownDuration;
        
        
        // Private Variables
        protected Animator animator;
        protected bool attackCoolDownActive;
        
        
        private void Awake()
        {
            Init();
        }


        protected virtual void Init()
        {
            animator = GetComponent<Animator>();

            attackCoolDownActive = false;
        }


        protected virtual void Update()
        {
            
        }
    }
}
