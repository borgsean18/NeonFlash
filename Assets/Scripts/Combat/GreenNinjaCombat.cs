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
            base.Attack();

            animator.SetTrigger("Attack");
        }


        protected override void MobileProjectileAttack()
        {
            base.MobileProjectileAttack();

            Debug.Log("FIRE PROJECTILE");
        }
    }
}
