using System;
using MainManagers;
using UnityEngine;

namespace Movement
{
    public class HorizMovement : MonoBehaviour
    {
        // Exposed Variables
        [Header("Settings")]
        [SerializeField] protected float movementSpeed;
        [SerializeField] protected bool moveWithWorld;
        
        // Private Variables
        protected WorldManager worldManager;
        private bool init;


        protected virtual void Awake()
        {
            Init();
        }


        private void Init()
        {
            if (moveWithWorld)
                worldManager = FindObjectOfType<WorldManager>();

            init = true;
        }


        // Update is called once per frame
        void Update()
        {
            if (init)
                SideScroll();
        }
        
        
        protected virtual float MovementSpeed()
        {
            if (moveWithWorld)
            {
                movementSpeed = worldManager.CurrentSpeed * Time.deltaTime;

                movementSpeed *= -1;
            }
		    
            return movementSpeed;
        }

        
        /// <summary>
        /// Virtual method used to move objects sideways only in the world.
        /// Positive MovementSpeed will move the objects right, negative movement
        /// speed will move them left.
        /// </summary>
        private void SideScroll()
        {
            if (movementSpeed == 0 && !moveWithWorld) return;
            
            // Calculate the movement distance based on speed and time
            float movementDistance = MovementSpeed();

            // Get the current position of the GameObject
            Vector3 currentPosition = transform.position;

            // Calculate the new position
            Vector3 newPosition = currentPosition + new Vector3(movementDistance, 0, 0);

            // Move the GameObject to the new position
            transform.position = newPosition;
        }
    }
}
