using System;
using UnityEngine;
using UnityEngine.Events;

namespace CleanUp
{
    public class CleanUp : MonoBehaviour
    {
        [SerializeField] private UnityEvent cleanUpEvent;
        
        
        // Private Variables
        private CleanParent cleanParent;
        

        private void Awake()
        {
            Init();
        }


        private void Init()
        {
            cleanParent = transform.parent.GetComponent<CleanParent>();

            if (cleanParent != null)
                cleanUpEvent.AddListener(cleanParent.DestroySelf);
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