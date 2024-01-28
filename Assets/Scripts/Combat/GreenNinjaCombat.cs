using UnityEngine;

namespace Combat
{
    public class GreenNinjaCombat : PlayerCombatScript
    {
        // Private Variables
        private Animator animator;


        protected override void Init()
        {
            base.Init();

            animator = GetComponent<Animator>();
        }


        protected override void Attack()
        {
            animator.SetTrigger("Attack");

            StartCoroutine(CoolDown());
        }
    }
}
