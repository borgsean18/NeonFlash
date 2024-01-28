using UnityEngine;
using System.Collections;
using General;

namespace Combat
{
    public abstract class PlayerCombatScript : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] protected float coolDownDuration;
        
        
        // Private Variables
        protected bool attackCoolDownActive;
        private TouchDetector touchDetector;
        
        
        private void Awake()
        {
            Init();
        }


        protected virtual void Init()
        {
            attackCoolDownActive = false;

#if UNITY_ANDROID
            touchDetector = FindObjectOfType<TouchDetector>();
            touchDetector.AddPlayTouchBehaviour(MobileAttack);
#endif
        }


        protected virtual void Update()
        {
           
#if UNITY_WEBGL
            PcAttack()
#endif

        }


        protected virtual void PcAttack()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && !attackCoolDownActive)
            {
                Attack();
            }
        }


        /// <summary>
        /// This class is being called in the Update method of TouchDetector.cs
        /// </summary>
        protected virtual void MobileAttack()
        {
            // Do nothing if attack cool down is active, or if theres no touches
            if (attackCoolDownActive || Input.touchCount <= 0) return; 
            
            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Position of the touch
                    Vector2 pos = Input.GetTouch(i).position;

                    // If player touched the lower half of the screen
                    if (pos.y <= Screen.height / 2 && pos.x <= Screen.width / 2)
                    {
                        Attack();
                    }
                }
            }
        }


        protected abstract void Attack();


        protected IEnumerator CoolDown()
        {
            attackCoolDownActive = true;
            
            yield return new WaitForSeconds(coolDownDuration);
            
            attackCoolDownActive = false;
        }
    }
}
