using UnityEngine;
using System.Collections;
using General;

namespace Combat
{
    public abstract class PlayerCombatScript : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] protected float meleCoolDown;
        [SerializeField] protected float projectileCoolDown;
        
        
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
            touchDetector.AddPlayTouchBehaviour(MobileAttackDetector);
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
        private void MobileAttackDetector()
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

                    // If player touched the right half of the screen
                    if (pos.x > Screen.width / 2)
                    {
                        MobileProjectileAttack();
                    }
                }
            }
        }


        protected virtual void MobileProjectileAttack()
        {
            StartCoroutine(CoolDown(projectileCoolDown));
        }


        protected virtual void Attack()
        {
            StartCoroutine(CoolDown(meleCoolDown));
        }


        protected IEnumerator CoolDown(float _coolDown)
        {
            attackCoolDownActive = true;
            
            yield return new WaitForSeconds(_coolDown);
            
            attackCoolDownActive = false;
        }
    }
}
