using UnityEngine;

namespace Combat
{
    public abstract class TakeDamage : MonoBehaviour
    {
        // Exposed Variables
        [Header("General Settings")]
        [SerializeField] protected int hitPoints;

        [Header("Debug Settings")]
        [SerializeField] private bool _isImmortal;


        protected abstract void Awake();
        
        
        public virtual void RecieveDamage(int _damageAmount)
        {
            if (_isImmortal)
                return;

            hitPoints -= _damageAmount;

            if (hitPoints <= 0 && !_isImmortal)
                Die();
        }


        protected abstract void Die();
    }
}
