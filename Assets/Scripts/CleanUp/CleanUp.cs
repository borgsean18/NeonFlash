using UnityEngine;

namespace CleanUp
{
    public class CleanUp : MonoBehaviour
    {
        /// <summary>
        /// When an object triggers the Clean Up
        /// </summary>
        protected virtual void OnTriggerExit2D(Collider2D _other)
        {
            // If the segment has exited the clean up object bounds
            if (!_other.gameObject.CompareTag("CleanUp"))
                return;
            
            Destroy(gameObject);
        }
    }
}