using Combat;
using UnityEngine;

namespace Enemies.Drone
{
    public class DroneTakeDamage : TakeDamage
    {
        // Exposed Variables
        [Header("Drone Specifics")]
        [SerializeField] private DroneBehaviour droneBehaviour;
        
        
        protected override void Die()
        {
            droneBehaviour.Die();
        }
    }
}
