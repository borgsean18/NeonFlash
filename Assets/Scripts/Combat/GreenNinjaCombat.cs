using General;
using UnityEngine;

namespace Combat
{
    public class GreenNinjaCombat : PlayerCombatScript
    {
        // Private Variables
        private Animator animator;
        private TouchDetector touchDetector;


        protected override void Init()
        {
            base.Init();

            animator = GetComponent<Animator>();
            touchDetector = FindObjectOfType<TouchDetector>();
            touchDetector.AddPlayTouchBehaviour(MobileAttack);
        }


        protected override void Attack()
        {
            animator.SetTrigger("Attack");

            StartCoroutine(CoolDown());
        }
    }
}
