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
            
#if UNITY_ANDROID
            
            // Do nothing if attack cool down is active, or if theres no touches
            if (attackCoolDownActive || Input.touchCount <= 0) return; 
            
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Position of the touch
                    Vector2 pos = Input.GetTouch(i).position;

                    // If player touched the lower half of the screen
                    if (pos.y <= Screen.height / 2)
                    {
                        Attack();
                    }
                }
            }
            
#endif
        }


        public void Attack()
        {
            animator.SetTrigger("Attack");

            StartCoroutine(CoolDown());
        }
        

        public IEnumerator CoolDown()
        {
            attackCoolDownActive = true;
            
            yield return new WaitForSeconds(coolDownDuration);
            
            attackCoolDownActive = false;
        }
    }
}
