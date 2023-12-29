using UnityEngine;

namespace Combat
{
    public abstract class TakeDamage : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] protected int hitPoints;
        
        
        public virtual void TakeDamageMethod(int _damageAmount)
        {
            hitPoints -= _damageAmount;

            if (hitPoints <= 0)
                Die();
        }


        protected abstract void Die();
    }
}
