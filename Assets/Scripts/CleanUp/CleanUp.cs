using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace CleanUp
{
    public class CleanUp : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private bool hasNestedClean;
        [SerializeField] private UnityEvent cleanUpEvent;
        
        
        // Private Variables
        private NestedClean cleanParent;
        

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            if (hasNestedClean)
            {
                cleanParent = transform.parent.GetComponent<NestedClean>();
                cleanUpEvent.AddListener(cleanParent.DestroySelf);
            }
        }
        

        /// <summary>
        /// When an object triggers the Clean Up
        /// </summary>
        protected virtual void OnTriggerExit2D(Collider2D _other)
        {
            // If the segment has exited the clean up object bounds
            if (!_other.gameObject.CompareTag("CleanUp"))
                return;
            
            cleanUpEvent.Invoke();
            
            Destroy(gameObject);
        }
    }
}