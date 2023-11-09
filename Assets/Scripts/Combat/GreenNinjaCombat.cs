using System.Collections;
using UnityEngine;

namespace Combat
{
    public class GreenNinjaCombat : CombatScript, IMele
    {
        protected override void Update()
        {
            base.Update();
            
#if UNITY_WEBGL
            if (Input.GetKeyDown(KeyCode.Mouse0) && !attackCoolDownActive)
            {
                Attack();
            }
#endif
        }


        public void Attack()
        {
            animator.SetTrigger("Attack");
        }
        

        public IEnumerator CoolDown()
        {
            attackCoolDownActive = true;
            
            yield return new WaitForSeconds(coolDownDuration);
            
            attackCoolDownActive = false;
        }
    }
}
