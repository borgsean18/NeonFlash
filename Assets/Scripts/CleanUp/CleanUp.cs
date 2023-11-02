using System;
using UnityEngine;
using UnityEngine.Events;

namespace CleanUp
{
    public class CleanUp : MonoBehaviour
    {
        // Exposed Variables
        [SerializeField] private bool hasCleanUpParent;
        [SerializeField] private UnityEvent cleanUpEvent;
        
        
        // Private Variables
        private CleanParent cleanParent;
        

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            if (hasCleanUpParent)
            {
                cleanParent = transform.parent.GetComponent<CleanParent>();
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