using UnityEngine;

namespace Combat
{
    public abstract class TakeDamage : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] protected int hitPoints;


        protected virtual void Awake()
        {

        }
        
        
        public virtual void RecieveDamage(int _damageAmount)
        {
            hitPoints -= _damageAmount;

            if (hitPoints <= 0)
                Die();
        }


        protected abstract void Die();
    }
}
